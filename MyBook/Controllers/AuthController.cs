using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBook.Entity;
using MyBook.Models;
using MyBook.Services.EmailServices;

namespace MyBook.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailService _emailService;
    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Register() =>
        View();
    
    [HttpGet]
    public IActionResult Login() =>
        View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(ModelState.IsValid)
        {
            var user = new User
            {
                SubId = 4,
                SubDateStart = default(DateTime),
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                LastName = model.Lastname,
                Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png")),
                EmailConfirmed = false,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.AddToRoleAsync(user, "User");

                var link = Url.Action(nameof(VerifyEmail), "Auth", new { userId = user.Id, code },Request.Scheme,Request.Host.ToString());

                var message = new Message(new string[] { model.Email }, "Подтверждение почты", $"<h2>Добро пожаловать на MyBook!</h2><br><p>Пожалуйста, подтвердите свою почту, перейдя по ссылке</p><a href='{link}'>Подтвердить регистрацию</a>");

                await _emailService.SendEmailAsync(message);
                
                return RedirectToAction("EmailVerification");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }
    
    public async Task<IActionResult> VerifyEmail(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return RedirectToAction("PageNotFound", "Home");

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
            return View();

        return RedirectToAction("PageNotFound", "Home");
    }

    [HttpGet]
    public IActionResult EmailVerification() => View();

    [HttpPost] 
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.
                PasswordSignInAsync(model.Email, model.Password, true, false);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult RestoreAccess() =>
        View();
}