using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YHVegeterianFoodOrderingSystem.Models
{
    public class register
    {
        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Fullname")]
        [StringLength(100, ErrorMessage = "Should be more than 6 chars and less than 100 chars", MinimumLength = 6)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The password must be at least 6 and at max 20 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The phone number should be 10 numbers.", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Please select a role")]
        public string Role { get; set; }

    }
}
