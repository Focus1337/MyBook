using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Models;

namespace MyBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index() =>
        View(await _context.Books.Include(a => a.Author).ToListAsync());

    public IActionResult PageNotFound() => 
        View();

    public IActionResult AccessDenied() =>
        View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => 
        View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}