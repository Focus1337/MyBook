using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Entity;

namespace MyBook.Controllers;

public class SubController : Controller
{
    private readonly ApplicationContext _context;

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public SubController(ApplicationContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index() =>
        View(await _context.Subs.Where(v => !v.Name.Equals("Бесплатно")).ToListAsync());

    public IActionResult Payment()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Pay(int SubId)
    {
        var curUser = await _userManager.GetUserAsync(HttpContext.User);

        var user = _context.Users.Find(curUser.Id);
        user.SubId = SubId;
        user.SubDateStart = DateTime.Now;
        _context.Users.Update(user);
        
        await _userManager.AddToRoleAsync(curUser, "UserSub");
        await _context.SaveChangesAsync();
        
        return default;
    }
}