using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YHVegeterianFoodOrderingSystem.Data;
using YHVegeterianFoodOrderingSystem.Models;

namespace YHVegeterianFoodOrderingSystem.Views.ViewMenu
{
    public class ViewMenusController : Controller
    {
        private readonly YHVegeterianFoodOrderingSystemContextNew _context;

        public ViewMenusController(YHVegeterianFoodOrderingSystemContextNew context)
        {
            _context = context;
        }

        // GET: ViewMenus
        public async Task<IActionResult> Index()
        {
            //Add the food name into dropdown list
            IQueryable<string> TypeQuery = from m in _context.Menu
                                           orderby m.FoodName
                                           select m.FoodName;
            List<string> foodName = new List<string>(await TypeQuery.Distinct().ToListAsync());
            ViewBag.FoodName = foodName;

            return View();
        }

        /*public IActionResult Index()
        {
            List<Menu> menus = (from food in this._context.Menu.Take(10)
                                select food).ToList();
            List<SelectListItem> FoodList = new List<SelectListItem>();
            foreach(var m in menus)
            {
                FoodList.Add(new SelectListItem()
                {
                    Text = m.FoodName,
                    Value = m.FoodName
                });
                ViewBag.FoodName = FoodList;
            }
            return View(new PurchaseHistory());
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PurchaseHistory model)
        {
            if (ModelState.IsValid)
            {
                PurchaseHistory obj = new PurchaseHistory()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerName = model.CustomerName,
                    PurchasedFood = model.PurchasedFood,
                    UnitPrice = model.UnitPrice,
                    Quantity = model.Quantity,
                    TotalPrice = model.TotalPrice
                };
                _context.PurchaseHistory.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ViewMenus");
            }
            return View(model);
        }
    }
}