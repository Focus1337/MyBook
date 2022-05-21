using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBook.DataAccess;
using MyBook.Entity;
using MyBook.Models;
using Newtonsoft.Json;

namespace MyBook.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _context;
    private readonly ILogger<AdminController> _logger;

    public AdminController(UserManager<User> userManager, ILogger<AdminController> logger, ApplicationContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index() =>
        View(_userManager.Users.ToList());

    [HttpGet]
    public IActionResult CreateUser() =>
        View();

    [HttpPost]
    public async Task<IActionResult> CreateUser(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                SubId = 4,
                SubDateStart = default,
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                LastName = model.LastName,
                Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png")),
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                _logger.LogInformation("admin \"{Admin}\" created new user - {User}",
                    User.Identity!.Name, JsonConvert.SerializeObject(user.UserName));

                // return RedirectToAction("Index");
                ModelState.AddModelError("", "Пользователь создан успешно");
            }
            else
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new AdminEditUserViewModel
            {Id = id, Name = user.Name, LastName = user.LastName, Email = user.Email};
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(AdminEditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                var oldEmail = user.Email;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.Name;
                user.LastName = model.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Изменения сохранены!");

                    _logger.LogInformation("admin \"{Admin}\" edited user - {User} | New email: {New}",
                        User.Identity!.Name, JsonConvert.SerializeObject(oldEmail),
                        JsonConvert.SerializeObject(user.UserName));
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                _logger.LogInformation("admin \"{Admin}\" deleted user - {User}",
                    User.Identity!.Name, JsonConvert.SerializeObject(user.UserName));
        }

        return RedirectToAction("Index");
    }

    

    [HttpGet]
    public IActionResult AuthorsModeration() =>
        View(_context.Authors.ToList());
    
    [HttpGet]
    public IActionResult BooksModeration() =>
        View(_context.Books.ToList());
}