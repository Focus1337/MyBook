using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;

namespace MyBook.Controllers;

public class SubController : Controller
{
    private readonly ApplicationContext _context;

    public SubController(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index() =>
        View(await _context.Subs.Where(v => !v.Name.Equals("Бесплатно")).ToListAsync());
    
    public IActionResult Payment()
    {
        return View();
    }
}