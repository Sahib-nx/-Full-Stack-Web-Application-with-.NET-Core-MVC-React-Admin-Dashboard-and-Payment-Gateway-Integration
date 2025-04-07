using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.ViewModel;
using CRM.Services;
using CRM.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CRM.Controllers
{
    public class UserController : Controller
    {
        private readonly SqlDbContext _dbcontext;
        private readonly ITokenService _tokenService;
        private readonly HybridModel _viewModel;

        public UserController(SqlDbContext dbContext, ITokenService tokenService)
        {

            _dbcontext = dbContext;
            _tokenService = tokenService;
            _viewModel = new HybridModel
            {
                Navbar = new NavbarModel { Isloggedin = false },
                Products = []
            };

        }

        // public string ErrorMessage = "";
        // public string SuccessMessage = "";

        // GET: UserController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            var token = Request.Cookies["JusTryingToDo"];
            if (string.IsNullOrEmpty(token))
            {
                return View(_viewModel);
            }


            var userId = _tokenService.VerifyTokenGetId(token);
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
            if (user.Role == Types.Role.Seller)
            {
                return RedirectToAction("SellerDashboard", "Seller");
            }
            else if (user.Role == Types.Role.Buyer)
            {
                return RedirectToAction("BuyerDashboard", "Buyer");
            }
            else
            {
                return View(_viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var token = Request.Cookies["JusTryingToDo"];
            if (string.IsNullOrEmpty(token))
            {
                return View(_viewModel);
            }

            var userId = _tokenService.VerifyTokenGetId(token);
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
            if (user.Role == Types.Role.Seller)
            {
                return RedirectToAction("SellerDashboard", "Seller");
            }
            else if (user.Role == Types.Role.Buyer)
            {
                return RedirectToAction("BuyerDashboard", "Buyer");
            }
            else
            {
                return View(_viewModel);
            }
        }




        // -------------------------------------------------------LOgin Register------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var findUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (findUser != null)
                {

                    ViewData["ErrorMessage"] = "Email already exists";
                    return View("Register");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                if (ModelState.IsValid)
                {

                    await _dbcontext.Users.AddAsync(user);
                    await _dbcontext.SaveChangesAsync();
                    ViewData["SuccessMessage"] = "User created successfully";
                    return View();

                }
                else
                {

                    ViewData["ErrorMessage"] = "All feilds are required!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: ";
                Console.WriteLine(ex.Message);
                return View();
            }


        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var findUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (findUser == null)
                {

                    ViewData["ErrorMessage"] = "Invalid email or password";
                    return View(_viewModel);
                }

                var verifyPassword = BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password);
                if (verifyPassword)
                {

                    var token = _tokenService.CreateToken(findUser.UserId.ToString(), findUser.Email, findUser.UserName);
                    HttpContext.Response.Cookies.Append(

               "JusTryingToDo",
               token,
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.Strict,
                   Expires = DateTimeOffset.UtcNow.AddMonths(6)

               }
            );
                }
                ViewData["SuccessfullMessage"] = "Logged in Successfully";

                if (findUser.Role == Types.Role.Admin)
                {
                    return RedirectToAction("AdminDashboard", "Admin");

                }
                else if (findUser.Role == Types.Role.Seller)
                {
                    return RedirectToAction("SellerDashboard", "Seller");
                }
                else
                {
                    return RedirectToAction("BuyerDashboard", "Buyer");
                }



            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }


        // -----------------------------Role Chnage----------------------------------------Role Chnage---------------------------------------------------



        [HttpGet]
        public async Task<IActionResult> ChangeRoleToSeller()
        {

            var token = Request.Cookies["JusTryingToDo"];

            if (string.IsNullOrEmpty(token))
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

            Console.Write(userId);

            if (user.Role == Types.Role.Buyer)
            {
                user.Role = Types.Role.Seller;
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("SellerDashboard", "Seller");

            }


            return RedirectToAction("Login");


        }


        [HttpGet]
        public async Task<IActionResult> ChangeRoleToBuyer()
        {


            var token = Request.Cookies["JusTryingToDo"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login");
            }

            var userId = _tokenService.VerifyTokenGetId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login"); // Ensure token verification returns valid userId
            }

            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
            if (user == null)
            {
                return RedirectToAction("login");
            }

            if (user.Role == Types.Role.Seller)
            {
                user.Role = Types.Role.Buyer;
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("BuyerDashboard", "Buyer");
            }

            return RedirectToAction("Login");


        }

        // -----------------------------User Edit----------------------------------------User Edit---------------------------------------------------


        [HttpPost]
        public async Task<IActionResult> EditUser(User EditedUser)
        {

            try
            {
                var token = Request.Cookies["JusTryingToDo"];
                if (string.IsNullOrEmpty(token))
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
                    return NotFound(); // User not found in database
                }

                user.UserName = EditedUser.UserName;
                user.Email = EditedUser.Email;
                user.Role = EditedUser.Role;

                // Hash new password only if it is changed
                if (!string.IsNullOrWhiteSpace(EditedUser.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(EditedUser.Password);
                }

                _dbcontext.Update(user);
                await _dbcontext.SaveChangesAsync();

                ViewData["SuccessfullMessage"] = "User Edited Successfully";
                return View();


            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }

        }







        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("JusTryingToDo");
            return RedirectToAction("index", "home");
        }









    }
}
