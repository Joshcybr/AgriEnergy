using Microsoft.AspNetCore.Mvc.Rendering; 

namespace AgriEnergy.Models
{
    public class EmployeeDashboardViewModel
    {
        public List<Product> Products { get; set; }
        public SelectList Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}