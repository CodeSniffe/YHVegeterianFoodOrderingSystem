using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YHVegeterianFoodOrderingSystem.Data;
using YHVegeterianFoodOrderingSystem.Models;
using System.Web;

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
            Menu objMenu = new Menu();

            //var objSingleModel = new <IEnumerable<SelectListItem>>
                //(objMenu.Price)

            //Add the food name into dropdown list
            IQueryable<string> TypeQuery = from m in _context.Menu
                                           orderby m.FoodName
                                           select m.FoodName;
            IEnumerable<SelectListItem> foodName = new SelectList(await TypeQuery.Distinct().ToListAsync());
            ViewBag.FoodName = foodName;

            //Filter out the price of the selected food name
            //IQueryable<decimal> PriceQuery = from m in _context.Menu
                                           //where m.FoodName == Food
                                           //select m.Price;
            //IEnumerable<SelectListItem> foodPrice = new SelectList(await PriceQuery.Distinct().ToListAsync());
            //ViewBag.UnitPrice = foodPrice;


            return View(await _context.Menu.ToListAsync());
        }

        // GET: ViewMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: ViewMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViewMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Price,FoodImagePath")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: ViewMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: ViewMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,Price,FoodImagePath")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: ViewMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: ViewMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }
    }
}
