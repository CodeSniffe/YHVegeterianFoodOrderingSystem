using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YHVegeterianFoodOrderingSystem.Areas.Identity.Data;
using YHVegeterianFoodOrderingSystem.Models;

namespace YHVegeterianFoodOrderingSystem.Controllers
{
    public class UserInfoController : Controller
    {
        private UserManager<YHVegeterianFoodOrderingSystemUser> userManager;

        public UserInfoController(UserManager<YHVegeterianFoodOrderingSystemUser> usrManager)
        {
            userManager = usrManager;
        }

        public IActionResult Index(string msg = null)
        {
            ViewBag.msg = msg;
            return View(userManager.Users);
        }

        public IActionResult registerUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> registerUser(StaffAddUserData user)
        {
            if(ModelState.IsValid)
            {
                YHVegeterianFoodOrderingSystemUser webUser = new YHVegeterianFoodOrderingSystemUser
                {
                    Email = user.Email,
                    DOB = user.DOB,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(webUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "UserInfo");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public async Task<ActionResult> Update(string id)
        {
            YHVegeterianFoodOrderingSystemUser user = await userManager.FindByIdAsync(id);
            Boolean a = false;
            Boolean b = false;
            Boolean c = false;
            
            if(user.Role == "Staff")
            {
                a = true;
            }
            else if(user.Role == "Customer")
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
        public async Task<IActionResult> Update(string id, string fullname, DateTime dob, string role,string email)
        {
            YHVegeterianFoodOrderingSystemUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.FullName = fullname;
                user.DOB = dob;
                user.Role = role;
                user.Email = email;

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

        public async Task<IActionResult> Delete(string id)
        {
            YHVegeterianFoodOrderingSystemUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            else
            {
                return RedirectToAction("Index", "UserInfo", new { msg = "Unable to delete user" });
            }
            return RedirectToAction("Index", "UserInfo", new { msg = "User deleted!" });
        }
    }
}
