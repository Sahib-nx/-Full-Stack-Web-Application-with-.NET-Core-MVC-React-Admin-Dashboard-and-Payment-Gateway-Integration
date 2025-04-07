using CRM.Data;
using CRM.Interfaces;
using CRM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    public class BuyerController : Controller
    {


        private readonly SqlDbContext _dbcontext;
        private readonly ITokenService _tokenService;
        private readonly HybridModel _viewModel;

        public BuyerController(SqlDbContext dbContext, ITokenService tokenService)
        {

            _dbcontext = dbContext;
            _tokenService = tokenService;
            _viewModel = new HybridModel
            {
                Navbar = new NavbarModel { UserRole = Types.Role.Buyer, Isloggedin = true },
                Products = [],
                CartProducts = []
            };

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> BuyerDashboard()
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
                    return RedirectToAction("Login");
                }
                // 💡 Send data to view
                ViewBag.UserName = user.UserName;   // or user.FullName if you have
                ViewBag.UserEmail = user.Email;


                if (user.Role == Types.Role.Buyer)
                {
                    _viewModel.Navbar.UserRole = Types.Role.Buyer;
                    _viewModel.Navbar.Isloggedin = true;
                    return View(_viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }




            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();// Return the view with the error message
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {

            var token = Request.Cookies["JusTryingToDo"];
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = _tokenService.VerifyTokenGetId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var cart = await _dbcontext.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.BuyerId.ToString() == userId);
            if (cart == null || cart.Products.Count == 0)
            {
                ViewBag.cartEmpty = "Cart is Empty";
                return View(_viewModel);
            }
            return View(_viewModel);

        }



    }
}
