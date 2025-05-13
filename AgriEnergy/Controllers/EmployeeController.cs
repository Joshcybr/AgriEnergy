using Microsoft.AspNetCore.Mvc;

namespace AgriEnergy.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
