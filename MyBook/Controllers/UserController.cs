using Microsoft.AspNetCore.Mvc;

namespace MyBook.Controllers;

public class UserController : Controller
{
    public IActionResult UserProfile()
    {
        return View();
    }
}