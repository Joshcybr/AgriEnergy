using Microsoft.AspNetCore.Mvc;
using AgriEnergy.Models;

public class FarmersController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View(); // Looks in Views/Farmers/Create.cshtml
    }

    [HttpPost]
    public IActionResult Create(Farmer model)
    {
        if (ModelState.IsValid)
        {
            // Save farmer logic
            return RedirectToAction("FarmerDashboard", "Dashboard");
        }
        return View(model);
    }
}
