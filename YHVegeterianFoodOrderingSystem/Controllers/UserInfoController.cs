using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YHVegeterianFoodOrderingSystem.Areas.Identity.Data;

namespace YHVegeterianFoodOrderingSystem.Controllers
{
    public class UserInfoController : Controller
    {
        private UserManager<YHVegeterianFoodOrderingSystemUser> userManager;

        public UserInfoController(UserManager<YHVegeterianFoodOrderingSystemUser> usrManager)
        {
            userManager = usrManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public async Task<ActionResult> Update(string id)
        {
            YHVegeterianFoodOrderingSystemUser user = await userManager.FindByIdAsync(id);
            Boolean a = false;
            Boolean b = false;
            Boolean c = false;
            
            if(user.userrole == "Staff")
            {
                a = true;
            }
            else if(user.userrole == "Customer")
            {
                b = true;
            }
            else
            {
                c = true;
            }
            ViewBag.users = new List<SelectListItem>
            {
                new SelectListItem {Selected = c, Text ="Select Option",Value="" },
                new SelectListItem {Selected = b, Text ="Staff",Value="Staff" },
                new SelectListItem {Selected = a, Text ="Customer",Value="Customer" }
            };
            if(user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, DateTime dob, String address, string userrole)
        {
            YHVegeterianFoodOrderingSystemUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                user.DOB = dob;
                user.Address = address;
                user.userrole = userrole;

                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View(user);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }
    }
}
