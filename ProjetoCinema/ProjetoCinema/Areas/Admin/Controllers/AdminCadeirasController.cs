using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ReflectionIT.Mvc.Paging;
using System.Data;

namespace ProjetoCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCadeirasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCadeirasController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Cadeiras.Include(s=>s.Salas).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadeiras = await _context.Cadeiras.Include(s => s.Salas).FirstOrDefaultAsync(m => m.Id == id);

            if (cadeiras == null)
            {
                return NotFound();
            }

            return View(cadeiras);
        }

        // GET: Admin/AdminLanches/Create
        public IActionResult Create()
        {
            ViewData["SalasId"] = new SelectList(_context.Salas.OrderBy(l=>l.Nome), "Id", "Nome");

            return View();
        }

        // POST: Admin/AdminFilmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int qtde_cadeiras,int qtde_filheiras,int salaId,[Bind("Id,Nome,SalasId")] Cadeiras cadeiras)
        {
            
            int qtde_por_filheira=qtde_cadeiras/qtde_filheiras,letra=65;
            var salas = _context.Salas.FirstOrDefault(l => l.Id==salaId) ;
            
            for(int i = 1; i <= qtde_filheiras;i++)
            {
                for(int j = 1;j<=qtde_por_filheira;j++)
                {
                    cadeiras.Nome = Convert.ToChar(letra)+j.ToString();
                    cadeiras.Salas = salas;
                    cadeiras.Status = true;
                    var verificar = _context.Cadeiras.FirstOrDefault(l => l.Nome.Equals(cadeiras.Nome) && salas.Id==l.Salas.Id);
                    if(verificar == null)
                    {
                        _context.Add(cadeiras);
                        await _context.SaveChangesAsync();
                    }
                    //new Cadeiras(""));
                    cadeiras.Salas = null;
                    cadeiras.Id = 0;
                }
                letra++;
            }
                
                return RedirectToAction(nameof(Index));


            //ViewData["SalasId"] = new SelectList(_context.Salas.OrderBy(l => l.Nome), "Id", "Nome");
            //return View(cadeiras);
        }

        // GET: Admin/AdminFilmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadeiras = await _context.Cadeiras.Include(s => s.Salas).FirstOrDefaultAsync(m => m.Id == id);
            if (cadeiras == null)
            {
                return NotFound();
            }
            
            return View(cadeiras);
        }

        // POST: Admin/AdminLanches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,SalasId")] Cadeiras cadeiras)
        {
            if (id != cadeiras.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(cadeiras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadeirasExists(cadeiras.Id))
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


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadeiras = await _context.Cadeiras.Include(s => s.Salas).FirstOrDefaultAsync(m => m.Id == id);
            if (cadeiras == null)
            {
                return NotFound();
            }

            return View(cadeiras);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cadeiras = await _context.Cadeiras.FindAsync(id);
            
            _context.Cadeiras.Remove(cadeiras);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadeirasExists(int id)
        {
            return _context.Cadeiras.Any(e => e.Id == id);
        }
    }
}
