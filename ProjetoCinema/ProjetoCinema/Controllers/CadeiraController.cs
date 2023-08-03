using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.ViewModels;

namespace ProjetoCinema.Controllers
{
    public class CadeiraController : Controller
    {
        private readonly AppDbContext _context;

        public CadeiraController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var filmes = _context.Filmes.Include(c=>c.Salas).Include(c=>c.Categoria).FirstOrDefault(f => f.Id == id);
            var sala = _context.Salas.FirstOrDefault(s => s.Id == filmes.Salas.Id);
            

            var lista = new FilmeSalasCadeirasViewModel()
            {
                filmes = filmes,
                salas = sala,
                cadeiras = null
            };
            
            ViewData["Cadeira"] = new SelectList(_context.Cadeiras.Where(l=>l.Salas.Id==sala.Id && l.Status==true), "Id", "Nome");

            return View("Index",lista);
        }

        public IActionResult Gravar(int cadeiraId)
        {

            return View();
        }
    }
}
