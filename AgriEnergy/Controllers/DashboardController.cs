using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AgriEnergy.Models;
using AgriEnergy.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriEnergy.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "FARMER")]
        [HttpGet]
        public async Task<IActionResult> FarmerDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            ViewBag.UserName = user.FullName ?? user.UserName;

            // Get the products created by this farmer
            var products = await _context.Products
                                         .Where(p => p.FarmerId == user.Id)
                                         .ToListAsync();

            return View(products);  // Pass the products to the view
        }

        // Only users in the EMPLOYEE role can access this dashboard
        
        public async Task<IActionResult> EmployeeDashboard()
        {
            var products = await _context.Products
                .Include(p => p.Farmer)
                .ToListAsync();

            // Get distinct categories for the filter dropdown
            var categories = await _context.Products
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToListAsync();

            ViewBag.Categories = new SelectList(categories);

            return View(products);
        }

    }
}
