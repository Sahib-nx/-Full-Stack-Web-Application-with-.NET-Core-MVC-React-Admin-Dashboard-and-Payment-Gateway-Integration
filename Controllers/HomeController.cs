using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using CRM.Data;
using CRM.Interfaces;
using CRM.Models.ViewModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqlDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly HybridModel _viewModel;

    public HomeController(SqlDbContext dbContext, ITokenService tokenService, ILogger<HomeController> logger)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        _logger = logger;
        _viewModel = new HybridModel
        {

            Navbar = new NavbarModel(),
            Products = []

        };
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = Request.Cookies["JusTryingToDo"];
        if (token == null)
        {
            return View(_viewModel);
        }

        var userId = _tokenService.VerifyTokenGetId(token);

        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.UserId.ToString() == userId);
        if (user != null)
        {

            _viewModel.Navbar.UserRole = user.Role;
            _viewModel.Navbar.Isloggedin = true;
        }

        return View(_viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Shop()
    {

        try
        {
            var products = await _dbContext.Products.Where(p => p.IsDeleted == false).ToListAsync();
            _viewModel.Products = products;

            return View(_viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HomeController Index method.");
            return View("Error");
        }

    }

    [HttpGet]
    public async Task<IActionResult> AboutUs()
    {

        try
        {
            var products = await _dbContext.Products.Where(p => p.IsDeleted == false).ToListAsync();
            _viewModel.Products = products;
            return View(_viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HomeController Index method.");
            return View("Error");
        }

    }

    [HttpGet]
    public async Task<IActionResult> ContactUs()
    {

        try
        {
            var products = await _dbContext.Products.Where(p => p.IsDeleted == false).ToListAsync();
            _viewModel.Products = products;
            return View(_viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HomeController Index method.");
            return View("Error");
        }

    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
