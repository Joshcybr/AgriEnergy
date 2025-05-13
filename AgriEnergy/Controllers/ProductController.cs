using AgriEnergy.Data;
using AgriEnergy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace AgriEnergy.Controllers
{
    [Authorize(Roles = "FARMER")]
   

        // GET: Product/Create
    
  
public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ProductController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;  // Injecting the logger
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create([Bind("Name,Price,Description")] Product product)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User is not authenticated.");
                return BadRequest("User is not authenticated.");
            }

            _logger.LogInformation($"UserId found: {userId}");

            // Set FarmerId manually
            product.FarmerId = userId;

            // Remove Farmer/FarmerId from validation check
            ModelState.Remove("Farmer");
            ModelState.Remove("FarmerId");

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Product saved successfully.");
                return RedirectToAction("FarmerDashboard", "Dashboards");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError("Model validation error: " + error.ErrorMessage);
            }

            return View(product);
        }



        // GET: Product/Index
        public async Task<IActionResult> FarmerDashboard()
        {
            var products = await _context.Products.ToListAsync();  // Fetch all products
            return View(products);  // Return the view with the list of products
        }
    }
}