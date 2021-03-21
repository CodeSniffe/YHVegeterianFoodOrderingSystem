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
            //IEnumerable<SelectListItem> foodName = new SelectList(await TypeQuery.Distinct().ToListAsync());
            ViewBag.FoodName = foodName;

            return View();
        }
    }
}
