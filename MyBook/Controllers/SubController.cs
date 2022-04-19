using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Entity;
using MyBook.Entity.Identity;

namespace MyBook.Controllers;

public class SubController : Controller
{
    private readonly ApplicationContext _context;

    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public SubController(ApplicationContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
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

        curUser.SubId = SubId;
        curUser.SubDateStart = DateTime.Now;

        await _userManager.UpdateAsync(curUser);
        await _userManager.AddToRoleAsync(curUser, "UserSub");
        
        return Ok(); // Redirect
    }
}