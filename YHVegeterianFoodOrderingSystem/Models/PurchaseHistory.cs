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
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PurchasedFood { get; set; }
        public string Quantity { get; set; }
        
        [Required(ErrorMessage = "Price is required!")]
        [RegularExpression("^[1-9]\\d{0,7}(?:\\.\\d{1,4})?$", ErrorMessage = "Numbers Only!")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 100, ErrorMessage = "Price should be in 1 ~ 100.")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }
}
