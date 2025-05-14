using Microsoft.AspNetCore.Mvc;
using AgriEnergy.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AgriEnergy.Controllers
{
    public class FarmersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public FarmersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddFarmerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    DateOfBirth = model.DateOfBirth,
                    CellPhone = model.CellPhone,
                    Region = model.Region,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "FARMER");
                    TempData["SuccessMessage"] = "New farmer added successfully!";
                    return RedirectToAction("EmployeeDashboard", "Dashboard");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}