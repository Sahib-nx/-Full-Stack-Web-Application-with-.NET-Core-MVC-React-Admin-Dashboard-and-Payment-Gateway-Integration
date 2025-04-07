using System.Threading.Tasks;
using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    public class SellerController : Controller
    {

        private readonly SqlDbContext _dbcontext;
        private readonly ITokenService _tokenService;
        private readonly HybridModel _viewModel;

        public SellerController(SqlDbContext dbContext, ITokenService tokenService)
        {
            _dbcontext = dbContext;
            _tokenService = tokenService;
            _viewModel = new HybridModel
            {
                Navbar = new NavbarModel { UserRole = Types.Role.Seller, Isloggedin = true },
                Products = []
            };
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View(_viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SellerDashboard()
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


                if (user.Role == Types.Role.Seller)
                {
                    _viewModel.Navbar.UserRole = Types.Role.Seller;
                    _viewModel.Navbar.Isloggedin = true;

                    return View(_viewModel);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult CreateProduct()
        {
            var token = Request.Cookies["JusTryingToDo"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }

            return View(_viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MyProducts()
        {

            var token = Request.Cookies["JusTryingToDo"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }

            var userId = _tokenService.VerifyTokenGetId(token);

            var myProduct = await _dbcontext.Products.Where(p => p.SellerId.ToString() == userId && p.IsArchived == false && p.IsDeleted == false).ToListAsync();
            _viewModel.Products = myProduct;
            return View(_viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> MyArchive()
        {
            var token = Request.Cookies["JusTryingToDo"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }
            var userId = _tokenService.VerifyTokenGetId(token);
            var archivedProduct = await _dbcontext.Products.Where(p => p.SellerId.ToString() == userId && p.IsArchived == true && p.IsDeleted == false).ToListAsync();
            _viewModel.Products = archivedProduct;
            return View(_viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> ArchiveProduct(Guid id)
        {

            var product = await _dbcontext.Products.FindAsync(id);

            if (product != null)
            {
                product.IsArchived = true;
                await _dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("MyProducts");

        }

        [HttpGet]
        public async Task<IActionResult> UnArchiveProduct(Guid id)
        {

            var product = await _dbcontext.Products.FindAsync(id);
            if (product != null)
            {
                product.IsArchived = false;
                await _dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("MyArchive");
        }

        [HttpGet]
        public async Task<IActionResult> DeletedProducts()
        {

            var token = Request.Cookies["JusTryingToDo"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }
            var userId = _tokenService.VerifyTokenGetId(token);
            var deletedProducts = await _dbcontext.Products.Where(p => p.SellerId.ToString() == userId && p.IsArchived == true && p.IsDeleted == true).ToListAsync();
            _viewModel.Products = deletedProducts;
            return View(_viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {

            var product = await _dbcontext.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                await _dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("MyArchive");
        }

        [HttpGet]
        public async Task<IActionResult> RestoreProduct(Guid id)
        {
            var product = await _dbcontext.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = false;
                await _dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("MyArchive");
        }



        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                var token = Request.Cookies["JusTryingToDo"];
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login", "User");
                }
                var userId = _tokenService.VerifyTokenGetId(token);

                var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
                if (user == null)
                {
                    return RedirectToAction("login", "User");
                }

                if (user.Role == Types.Role.Seller)
                {
                    product.SellerId = user.UserId;
                    _dbcontext.Products.Add(product);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction("MyProducts");
                }
                return RedirectToAction("login", "User");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }

        }


    }
}
