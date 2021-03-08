using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace YHVegeterianFoodOrderingSystem.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the YHVegeterianFoodOrderingSystemUser class
    public class YHVegeterianFoodOrderingSystemUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [PersonalData]
        public string Address { get; set; }

        [PersonalData]
        public string Role { get; set; }
    }
}
