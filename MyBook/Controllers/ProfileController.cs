using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Entity;
using MyBook.Models;

namespace MyBook.Controllers;

[Authorize(Roles = "User")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ProfileController> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationContext _context;

    public ProfileController(UserManager<User> userManager, ILogger<ProfileController> logger,
        SignInManager<User> signInManager, ApplicationContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        if (user == null)
            return NotFound();
        
        var sub = (await _context.Subs.FirstOrDefaultAsync(x => x.Id == user.SubId))!;

        var model = new EditProfileViewModel
        {
            Email = user.Email, Name = user.Name, LastName = user.LastName, Image = user.Image,
            Sub = sub,
            SubDurationLeft = sub.Duration - DateTime.Now.Subtract(user.SubDateStart).Days
        };
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name);
            if (user != null)
            {
                var result =
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);


                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Пароль изменён успешно!");
                    _logger.LogInformation("Пользователь \"{User}\" изменил свой пароль", user.UserName);
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            else
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> ChangeImage(ChangeProfileImageViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user != null)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                    imageData = binaryReader.ReadBytes((int) model.Image.Length);

                user.Image = Convert.ToBase64String(imageData);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");
                    _logger.LogInformation(
                        "Пользователь \"{User}\" обновил фотографию профиля", user.Email);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<ActionResult> ResetImage()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user != null)
            {
                user.Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png"));

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");
                    _logger.LogInformation(
                        "Пользователь \"{User}\" обновил фотографию профиля", user.Email);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        ModelState.Remove("Image");
        ModelState.Remove("Sub");
        ModelState.Remove("SubDurationLeft");
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user != null)
            {
                var sub = (await _context.Subs.FirstOrDefaultAsync(x => x.Id == user.SubId))!;
                var oldEmail = user.Email;
                if (user.Email != model.Email)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
                
                user.Name = model.Name;
                user.LastName = model.LastName;
                //roflan
                model.Image = user.Image;
                model.Sub = sub;
                model.SubDurationLeft = sub.Duration - DateTime.Now.Subtract(user.SubDateStart).Days;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");
                    _logger.LogInformation(
                        "Пользователь \"{User}\" обновил информацию профиля ({Email}, {Name}, {LastName})",
                        oldEmail, user.Email, user.Name, user.LastName);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }

        return View("Index", model);
    }
    
}