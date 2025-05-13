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
                return RedirectToAction("FarmerDashboard", "Dashboard"); // Redirect to the FarmerDashboard
            }

            // If validation fails, return to the create page
            return View(product);
        }


        // GET: Product/Index
        public async Task<IActionResult> FarmerDashboard()
        {
            var products = await _context.Products.ToListAsync();  // Fetch all products
            return View(products);  // Return the view with the list of products

        } // GET: Product/Edit/5


        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,Price,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Ensure the product belongs to the current user
            var existingProduct = await _context.Products
                .Where(p => p.Id == id && p.FarmerId == user.Id)
                .FirstOrDefaultAsync();

            if (existingProduct == null)
            {
                return NotFound(); // Prevent editing someone else's product
            }

            // Update fields
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("FarmerDashboard", "DashBoard");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem("A concurrency error occurred while trying to update the product.");
            }
        }


        // DELETE: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("FarmerDashboard", "Dashboard");  
        }
                                                                     
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}