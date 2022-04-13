using Microsoft.AspNetCore.Mvc;

namespace MyBook.Controllers;

public class PaymentController : Controller
{
    public IActionResult Paying()
    {
        return View();
    }
}