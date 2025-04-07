using CRM.Data;
using CRM.Interfaces;
using CRM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    public class AdminController : Controller
    {



        private readonly SqlDbContext _dbcontext;
        private readonly ITokenService _tokenService;
        private readonly HybridModel _viewModel;

        public AdminController(SqlDbContext dbContext, ITokenService tokenService)
        {

            _dbcontext = dbContext;
            _tokenService = tokenService;
            _viewModel = new HybridModel
            {
                Navbar = new NavbarModel { UserRole = Types.Role.Buyer, Isloggedin = false },
                Products = []
            };

        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AdminDashboard()
        {
            try
            {
                var token = Request.Cookies["JusTryingToDo"];
                if (token == null)
                {
                    return RedirectToAction("Login");
                }


                var userId = _tokenService.VerifyTokenGetId(token);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login"); // Ensure token verification returns valid userId
                }


                var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

                if (user == null)
                {
                    return RedirectToAction("Login"); // User not found in database
                }

                if (user.Role == Types.Role.Admin)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("login");
                }

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }

        }


    }
}
