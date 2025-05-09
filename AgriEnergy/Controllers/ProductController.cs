using AgriEnergy.Data;
using AgriEnergy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgriEnergy.Controllers
{
    [Authorize(Roles = "FARMER")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Product product)
{
    if (!ModelState.IsValid)
    {
        // Log validation errors
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine("Model Error: " + error.ErrorMessage);
        }

        return View(product);
    }

    var user = await _userManager.GetUserAsync(User);
    product.FarmerId = user.Id;

    _context.Add(product);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(FarmerDashboard), "Dashboard");
}



        // GET: Farmer Dashboard - Only show farmer's products
        public async Task<IActionResult> FarmerDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var products = await _context.Products
                .Where(p => p.FarmerId == user.Id)  // Filter products by the logged-in farmer
                .ToListAsync();

            ViewBag.UserName = user?.FullName ?? user?.UserName;

            return View(products);  // Pass the farmer's products to the view
        }
    }
}

