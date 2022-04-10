using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;

namespace MyBook.Controllers;

public class CatalogController : Controller
{
    private readonly ApplicationContext _context;
    
    public CatalogController(ApplicationContext context) => 
        _context = context;

    [HttpGet]
    public IActionResult Index() => 
        View();

    [HttpGet]
    public async Task<IActionResult> Books() =>
        View(await _context.Books.Include(a => a.Author).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> FreeBooks() =>
        View(await _context.Books
            .Include(a => a.Author)
            .Where(s => s.SubType == 0)
            .ToListAsync());
    
    [Authorize(Roles = "UserSub, Admin")]
    [HttpGet]
    public async Task<IActionResult> Premium() =>
        View(await _context.Books
            .Include(a => a.Author)
            .Where(s => s.SubType == 1)
            .ToListAsync());
    
    public IActionResult BookDetails() =>
        View();
}