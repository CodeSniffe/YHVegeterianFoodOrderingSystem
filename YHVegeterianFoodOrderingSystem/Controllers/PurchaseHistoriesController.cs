using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YHVegeterianFoodOrderingSystem.Data;
using YHVegeterianFoodOrderingSystem.Models;

namespace YHVegeterianFoodOrderingSystem.Views.PurchaseHistories
{
    public class PurchaseHistoriesController : Controller
    {
        private readonly YHVegeterianFoodOrderingSystemContextNew _context;

        public PurchaseHistoriesController(YHVegeterianFoodOrderingSystemContextNew context)
        {
            _context = context;
        }

        // GET: PurchaseHistories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseHistory.ToListAsync());
        }

        // GET: PurchaseHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return View(purchaseHistory);
        }


        // GET: PurchaseHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return View(purchaseHistory);
        }

        // POST: PurchaseHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);
            _context.PurchaseHistory.Remove(purchaseHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseHistoryExists(int id)
        {
            return _context.PurchaseHistory.Any(e => e.Id == id);
        }
    }
}
