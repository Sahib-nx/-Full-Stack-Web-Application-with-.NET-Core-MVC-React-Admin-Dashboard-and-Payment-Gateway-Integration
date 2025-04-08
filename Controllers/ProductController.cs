using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using CRM.Models.JunctionModel;
using CRM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    public class ProductController : Controller
    {


        private readonly SqlDbContext _dbcontext;
        private readonly ITokenService _tokenService;
        private readonly HybridModel _viewModel;

        public ProductController(SqlDbContext dbContext, ITokenService tokenService)
        {
            _dbcontext = dbContext;
            _tokenService = tokenService;
            _viewModel = new HybridModel
            {
                Navbar = new NavbarModel {Isloggedin = false },
            };
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _dbcontext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _viewModel.Product = product;

            return View(_viewModel);
        }

        [Authorize]  
        [HttpGet]
        public async Task<IActionResult> AddToCart(Guid ProductId)
        {

            try
            {
                // //Authorixation
                // var token = Request.Cookies["JusTryingToDo"];
                // if (token == null)
                // {
                //     return RedirectToAction("Login", "User");
                // }
                // var userId = _tokenService.VerifyTokenGetId(token);
                Guid? userId = HttpContext.Items["UserId"] as Guid?;

                //Then telling the seller they can't buy their own products
                var product = await _dbcontext.Products.FindAsync(ProductId);

                if (userId == product?.SellerId)
                {

                    ViewBag.errormessage = "You can't add your own product to cart!";
                    return View("Error" , _viewModel);

                }

                //if the user have no cart here we are creating the cart for him
                var cart = await _dbcontext.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.BuyerId == userId);

                if (cart == null)
                {

                    cart = new Cart
                    {
                        BuyerId = (Guid)userId,
                        CartValue = 0
                    };
                    await _dbcontext.Carts.AddAsync(cart);
                    await _dbcontext.SaveChangesAsync();
                }

                var existingCartProduct = await _dbcontext.CartProducts.FirstOrDefaultAsync(cp => cp.CartId == cart.CartId && cp.ProductId == ProductId);

                if (existingCartProduct == null)
                {

                    var cartProduct = new CartProduct
                    {
                        CartId = cart.CartId,
                        ProductId = ProductId,
                        Quantity = 1
                    };
                    await _dbcontext.CartProducts.AddAsync(cartProduct);
                    cart.CartValue += product.Price;
                    await _dbcontext.SaveChangesAsync();
                }

                if (existingCartProduct != null)
                {
                    existingCartProduct.Quantity += 1;
                    cart.CartValue += product.Price;
                    await _dbcontext.SaveChangesAsync();
                }

                return RedirectToAction("Cart", "Buyer");


            }
            catch (Exception ex)
            {
                 ViewBag.errorMessage = $"Server Error:  {ex.Message}" ;
                return View("Error" , _viewModel);

          
            }
        }



    }
}
