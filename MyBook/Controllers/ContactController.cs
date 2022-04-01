using Microsoft.AspNetCore.Mvc;

namespace MyBook.Controllers;

public class ContactController : Controller
{
    [HttpGet]
    public IActionResult Index() =>
        View();

    [HttpPost]
    public IActionResult Send()
    {
        // logic
        return Ok();
    }
}