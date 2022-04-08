using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;

namespace MyBook.Controllers;

public class CatalogController : Controller
{
    private readonly ApplicationContext _context;
    
    public CatalogController(ApplicationContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Books() =>
        View(await _context.Books.Include(x => x.Author).ToListAsync());

    public IActionResult BookDetails() =>
        View();
}