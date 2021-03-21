using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YHVegeterianFoodOrderingSystem.Models
{
    public class PurchaseHistory
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Customer Name")]
        [RegularExpression("^[a-zA-Z ]+", ErrorMessage = "Letters Only!")]
        [StringLength(100, ErrorMessage = "Should be more than 6 chars and less than 100 chars", MinimumLength = 6)]
        public string CustomerName { get; set; }
        public string PurchasedFood { get; set; }
        public string UnitPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [RegularExpression("^[0-9]+", ErrorMessage = "Numbers Only!")]
        [DataType(DataType.Text)]
        [Display(Name = "Quantity")]
        public string Quantity { get; set; }
        
        [Required(ErrorMessage = "Price is required!")]
        [RegularExpression("^[1-9]\\d{0,7}(?:\\.\\d{1,4})?$", ErrorMessage = "Numbers Only!")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 100, ErrorMessage = "Price should be in 1 ~ 100.")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

    }
}
