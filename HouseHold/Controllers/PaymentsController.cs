using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HouseHold.Data;
using HouseHold.Models;

namespace HouseHold.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = _context.Payments
                .Include(p => p.SubjectNameNavigation)
                .Include(p => p.PaymentTypeNavigation)
                .Include(p => p.PaymentMethodNavigation)
                .Include(p => p.IncomeCategoryNavigation)
                .Include(p => p.IncomeTypeNavigation);
            return View(await payments.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payments
                .Include(p => p.SubjectNameNavigation)
                .Include(p => p.PaymentTypeNavigation)
                .Include(p => p.PaymentMethodNavigation)
                .Include(p => p.IncomeCategoryNavigation)
                .Include(p => p.IncomeTypeNavigation)
                .FirstOrDefaultAsync(m => m.PaymentId == id.Value);
            if (payment == null) return NotFound();

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["SubjectNameId"] = new SelectList(_context.SubjectNames, "SubjectNameId", "CourseName");
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "MethodName");
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName");
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "IncomeTypeId", "TypeName");
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,Posted,InOut,SubjectNameId,ItemName,PaymentName,PaymentTypeId,PaymentMethodId,IncomeCategoryId,IncomeTypeId,Amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectNameId"] = new SelectList(_context.SubjectNames, "SubjectNameId", "CourseName", payment.SubjectNameId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName", payment.PaymentTypeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "MethodName", payment.PaymentMethodId);
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName", payment.IncomeCategoryId);
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "IncomeTypeId", "TypeName", payment.IncomeTypeId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payments.FindAsync(id.Value);
            if (payment == null) return NotFound();
            ViewData["SubjectNameId"] = new SelectList(_context.SubjectNames, "SubjectNameId", "CourseName", payment.SubjectNameId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName", payment.PaymentTypeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "MethodName", payment.PaymentMethodId);
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName", payment.IncomeCategoryId);
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "IncomeTypeId", "TypeName", payment.IncomeTypeId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Posted,InOut,SubjectNameId,ItemName,PaymentName,PaymentTypeId,PaymentMethodId,IncomeCategoryId,IncomeTypeId,Amount")] Payment payment)
        {
            if (id != payment.PaymentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["SubjectNameId"] = new SelectList(_context.SubjectNames, "SubjectNameId", "CourseName", payment.SubjectNameId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "TypeName", payment.PaymentTypeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "MethodName", payment.PaymentMethodId);
            ViewData["IncomeCategoryId"] = new SelectList(_context.IncomeCategories, "IncomeCategoryId", "CategoryName", payment.IncomeCategoryId);
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "IncomeTypeId", "TypeName", payment.IncomeTypeId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payments
                .Include(p => p.SubjectNameNavigation)
                .Include(p => p.PaymentTypeNavigation)
                .Include(p => p.PaymentMethodNavigation)
                .Include(p => p.IncomeCategoryNavigation)
                .Include(p => p.IncomeTypeNavigation)
                .FirstOrDefaultAsync(m => m.PaymentId == id.Value);
            if (payment == null) return NotFound();

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
