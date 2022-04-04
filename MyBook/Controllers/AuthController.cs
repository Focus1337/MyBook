using Microsoft.AspNetCore.Mvc;

namespace MyBook.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Register() =>
        View();
    
    [HttpGet]
    public IActionResult Login() =>
        View();

    // [HttpPost]
    // public IActionResult Login()
    // {
    //     // logic
    //     return Ok();
    // }
    
    [HttpGet]
    public IActionResult RestoreAccess() =>
        View();
}