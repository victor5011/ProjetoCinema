using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ReflectionIT.Mvc.Paging;

namespace ProjetoCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminSalasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminSalasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Salas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                  resultado = _context.Salas.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas = await _context.Salas.FirstOrDefaultAsync(m => m.Id == id);
            if (salas == null)
            {
                return NotFound();
            }

            return View(salas);
        }

        // GET: Admin/AdminLanches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminFilmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Salas salas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salas);
        }

        // GET: Admin/AdminFilmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //var filmes = _context.Filmes.AsQueryable();
            
            if (id == null)
            {
                return NotFound();
            }

            //var salas = await _context.Salas.inn.Where(l=>l.Id == filmes.).OrderBy(l=>l.Id).ToListAsync();
            var salas = await _context.Salas.FindAsync(id);

            if (salas == null)
            {
                return NotFound();
            }
            
            return View(salas);
        }

        // POST: Admin/AdminLanches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Salas salas)
        {
            if (id != salas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalasExists(salas.Id))
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
            return View(salas);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas = await _context.Salas.FirstOrDefaultAsync(m => m.Id == id);
            if (salas == null)
            {
                return NotFound();
            }

            return View(salas);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salas = await _context.Salas.FindAsync(id);
            
            _context.Remove(salas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalasExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
