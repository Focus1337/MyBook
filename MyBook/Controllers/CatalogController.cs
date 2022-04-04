using Microsoft.AspNetCore.Mvc;

namespace MyBook.Controllers;

public class CatalogController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BookDetails() =>
        View();
}