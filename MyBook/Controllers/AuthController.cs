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

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
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
        if (ModelState.IsValid)
        {
            var user = new User
            {
                SubId = 4,
                SubDateStart = default(DateTime),
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                LastName = model.LastName,
                Image = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("wwwroot/img/user.png")),
                EmailConfirmed = false,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.AddToRoleAsync(user, "User");

                var link = Url.Action(nameof(VerifyEmail), "Auth", new {userId = user.Id, code}, Request.Scheme,
                    Request.Host.ToString());

                var message = new Message(new[] {model.Email}, "Подтверждение почты",
                    $"<h2>Добро пожаловать на MyBook!</h2><br><p>Пожалуйста, подтвердите свою почту, перейдя по ссылке</p><a href='{link}'>Подтвердить регистрацию</a>");

                await _emailService.SendEmailAsync(message);

                return View("AuthStatus", "Письмо с подтверждением было отправлено на вашу почту");
            }
            else
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
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
            return View("AuthStatus", "Почта подтверждена! Теперь можно зайти");

        return RedirectToAction("PageNotFound", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> RestoreAccess(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(PasswordReset), "Auth", new {email = user.Email, token}, Request.Scheme,
                Request.Host.ToString());

            var message = new Message(new[] {email}, "Восстановление доступа",
                "<p>Для восстановления доступа к аккаунту вам нужно перейти по ссылке, чтобы сменить ваш пароль." +
                $"<br><a href='{link}'>Сменить пароль</a>");
            try
            {
                await _emailService.SendEmailAsync(message);
                return View("AuthStatus", "Письмо с инструкцией отправлена на вашу почту");
            }
            catch (MailKit.Net.Smtp.SmtpProtocolException)
            {
                return View("AuthStatus", "Сервис временно не работает.");
            }
        }

        return RedirectToAction("PageNotFound", "Home");
    }

    [HttpGet]
    public IActionResult RestoreAccess() =>
        View();

    [HttpPost]
    public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                    return View("AuthStatus", "Пароль изменён успешно");
                else
                    ModelState.AddModelError("", "Ошибка. Не удалось сменить пароль.");
            }
            else
                ModelState.AddModelError("", "Такого пользователя не существует");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult PasswordReset(string email, string token)
    {
        var model = new ResetPasswordViewModel
        {
            Email = email,
            Password = "",
            ConfirmPassword = "",
            Token = token
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult AuthStatus() =>
        View();
}