using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergy.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }  

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; } 



        public string FarmerId { get; set; }

        public ApplicationUser Farmer { get; set; }
    }
}
