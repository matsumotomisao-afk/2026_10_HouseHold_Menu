using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseHold.Data;
using HouseHold.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseHold.Controllers
{
    public class IncomeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncomeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeTypes.ToListAsync());
        }

        // GET: IncomeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var incomeType = await _context.IncomeTypes
                .FirstOrDefaultAsync(m => m.IncomeTypeId == id.Value);
            if (incomeType == null) return NotFound();

            return View(incomeType);
        }

        // GET: IncomeTypes/Create
        public IActionResult Create()
        {
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName");
            return View();
        }

        // POST: IncomeTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncomeTypeId,TypeName,IncomeCategoryId")] IncomeType incomeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName", incomeType.IncomeCategoryId);
            return View(incomeType);
        }

        // GET: IncomeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var incomeType = await _context.IncomeTypes.FindAsync(id.Value);
            if (incomeType == null) return NotFound();
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName", incomeType.IncomeCategoryId);
            return View(incomeType);
        }

        // POST: IncomeTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncomeTypeId,TypeName,IncomeCategoryId")] IncomeType incomeType)
        {
            if (id != incomeType.IncomeTypeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeTypeExists(incomeType.IncomeTypeId))
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
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName", incomeType.IncomeCategoryId);
            return View(incomeType);
        }

        // GET: IncomeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var incomeType = await _context.IncomeTypes
                .FirstOrDefaultAsync(m => m.IncomeTypeId == id.Value);
            if (incomeType == null) return NotFound();

            return View(incomeType);
        }

        // POST: IncomeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomeType = await _context.IncomeTypes.FindAsync(id);
            if (incomeType != null)
            {
                _context.IncomeTypes.Remove(incomeType);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeTypeExists(int id)
        {
            return _context.IncomeTypes.Any(e => e.IncomeTypeId == id);
        }
    }
}
