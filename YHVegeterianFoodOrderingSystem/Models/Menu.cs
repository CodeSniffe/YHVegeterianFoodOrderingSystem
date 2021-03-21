using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YHVegeterianFoodOrderingSystem.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Food name is required!")]
        [RegularExpression("^[a-zA-Z ]+", ErrorMessage = "Letters Only!")]
        [Display(Name = "Food Name")]
        [StringLength(60, ErrorMessage = "The name should be between 3 - 60chars!", MinimumLength = 3)]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [RegularExpression("^[1-9]\\d*(\\.\\d+)?$", ErrorMessage = "Numbers Only!")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 100, ErrorMessage = "Price should be in 1 ~ 100.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

    }
}
