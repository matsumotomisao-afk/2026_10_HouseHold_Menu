using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseHold.Data;
using HouseHold.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseHold.Controllers
{
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var items = await _context.Menus.ToListAsync();
            return View(items);
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus
                .Include(m => m.Parent)
                .FirstOrDefaultAsync(m => m.Id == id.Value);
            if (menu == null) return NotFound();

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Menus.OrderBy(m => m.SortOrder).ThenBy(m => m.Title), "Id", "Title");
            return View();
        }

        // POST: Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TitleEn,Url,Slug,SortOrder,IsActive,IsVisibleInMenu,IconClass,Description,DescriptionEn,Target,PermissionKey,ParentId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Menus.OrderBy(m => m.SortOrder).ThenBy(m => m.Title), "Id", "Title", menu.ParentId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus.FindAsync(id.Value);
            if (menu == null) return NotFound();
            ViewData["ParentId"] = new SelectList(_context.Menus.Where(m => m.Id != id.Value).OrderBy(m => m.SortOrder).ThenBy(m => m.Title), "Id", "Title", menu.ParentId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,TitleEn,Url,Slug,SortOrder,IsActive,IsVisibleInMenu,IconClass,Description,DescriptionEn,Target,PermissionKey,ParentId")] Menu menu)
        {
            if (id != menu.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Menus.Where(m => m.Id != id).OrderBy(m => m.SortOrder).ThenBy(m => m.Title), "Id", "Title", menu.ParentId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus
                .Include(m => m.Parent)
                .FirstOrDefaultAsync(m => m.Id == id.Value);
            if (menu == null) return NotFound();

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
