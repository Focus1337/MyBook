﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBook.Models;

namespace MyBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Subscription() =>
        View();
    
    
    public IActionResult PageNotFound() => 
        View();

    public IActionResult AccessDenied() =>
        View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => 
        View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}