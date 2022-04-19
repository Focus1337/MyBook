using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Services;

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
    
    [HttpGet]
    public async Task<IActionResult> TopBooks() =>
        View(await _context.Books
            .Include(a => a.Author)
            .ToListAsync());
    
    [Authorize(Roles = "UserSub, Admin")]
    [HttpGet]
    public async Task<IActionResult> Premium() =>
        View(await _context.Books
            .Include(a => a.Author)
            .Where(s => s.SubType == 1)
            .ToListAsync());
    
    public async Task<IActionResult> BookDetails(Guid id)
    {
        var book = await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
            return RedirectToAction("PageNotFound", "Home");
        
        return View(book);
    }
}