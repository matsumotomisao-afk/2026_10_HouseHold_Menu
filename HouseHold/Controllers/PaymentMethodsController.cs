using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseHold.Data;
using HouseHold.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseHold.Controllers
{
    public class PaymentMethodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentMethods.Include(p => p.PaymentType).ToListAsync());
        }

        // GET: PaymentMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var paymentMethod = await _context.PaymentMethods
                .Include(p => p.PaymentType)
                .FirstOrDefaultAsync(m => m.PaymentMethodId == id.Value);
            if (paymentMethod == null) return NotFound();

            return View(paymentMethod);
        }

        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName");
            return View();
        }

        // POST: PaymentMethods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentMethodId,MethodName,PaymentTypeId")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName", paymentMethod.PaymentTypeId);
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var paymentMethod = await _context.PaymentMethods.FindAsync(id.Value);
            if (paymentMethod == null) return NotFound();
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName", paymentMethod.PaymentTypeId);
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentMethodId,MethodName,PaymentTypeId")] PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.PaymentMethodId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.PaymentMethodId))
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
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName", paymentMethod.PaymentTypeId);
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var paymentMethod = await _context.PaymentMethods
                .Include(p => p.PaymentType)
                .FirstOrDefaultAsync(m => m.PaymentMethodId == id.Value);
            if (paymentMethod == null) return NotFound();

            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod != null)
            {
                _context.PaymentMethods.Remove(paymentMethod);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(int id)
        {
            return _context.PaymentMethods.Any(e => e.PaymentMethodId == id);
        }
    }
}
