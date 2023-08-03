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
    public class AdminFilmesController : Controller
    {
        private readonly AppDbContext _context;
        private string caminhoServidor;

        public AdminFilmesController(AppDbContext context,IWebHostEnvironment sistema)
        {
            _context = context;
            caminhoServidor = sistema.WebRootPath;
        }

        // GET: Admin/AdminLanches
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Filmes.Include(l => l.Categoria);
        //    return View(await appDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageindex=1,string sort="Nome")
        {
            var resultado = _context.Filmes.Include(l => l.Categoria).Include(l=>l.Salas).AsQueryable();

            if(!string.IsNullOrWhiteSpace(filter))
            {
                resultado=resultado.Where(p=>p.Nome.Contains(filter));  
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }


        // GET: Admin/AdminLanches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filmes == null)
            {
                return NotFound();
            }

            return View(filmes);
        }

        // GET: Admin/AdminLanches/Create
        public IActionResult Create()
        {
            ViewData["Categoria"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["Salas"] = new SelectList(_context.Salas, "Id", "Nome");

            return View();
        }

        // POST: Admin/AdminFilmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile foto,int categoriaid,int salasid,[Bind("Id,Nome,Sinopse,Status,ImagemUrl,ImagemThumbnailUrl,FilmeDestaque,Duracao,DataInicial,DataFinal")] Filmes filmes)
        {
            var categoria = _context.Categorias.FirstOrDefault(l => l.Id == categoriaid);
            var salas = _context.Salas.FirstOrDefault(l => l.Id == salasid);
            filmes.Status = true;
            filmes.Categoria = categoria;
            filmes.Salas = salas;
            var filmesLista = _context.Filmes.AsNoTracking().Include(s=>s.Salas).Where(f=>f.Status==true).ToList();
            var filmeBusca = filmesLista.FirstOrDefault(c=>c.DataInicial==filmes.DataInicial && c.Status==true && c.Salas.Id==filmes.Salas.Id);

            if(filmeBusca==null)
            {
                filmeBusca=filmesLista.FirstOrDefault(f=>f.Status==true && filmes.DataInicial>=f.DataInicial && filmes.DataInicial<=f.DataFinal && f.Salas.Id==filmes.Salas.Id);
                
                if(filmeBusca==null)
                {
                    

                    string caminhoParaSalvarImagem = caminhoServidor + "\\imagens\\site\\";
                    string novoNome = Guid.NewGuid().ToString() + "_" + foto.FileName;

                    if (!Directory.Exists(caminhoParaSalvarImagem))
                    {
                        Directory.CreateDirectory(caminhoParaSalvarImagem);
                    }

                    using (var stream = System.IO.File.Create(caminhoParaSalvarImagem + novoNome))
                    {
                        foto.CopyToAsync(stream);
                    }

                    filmes.ImagemThumbnailUrl = "/imagens/site/" + novoNome;
                    filmes.ImagemUrl = "/imagens/site/" + novoNome;

                    _context.Add(filmes);
                    await _context.SaveChangesAsync();

                    ViewData["Categoria"] = new SelectList(_context.Categorias, "Id", "Nome");
                    ViewData["Salas"] = new SelectList(_context.Salas, "Id", "Nome");
                }
                else
                {
                    //ViewData["Erros"] = "Já existe um filme sendo exibido nessa data nessa sala, por favor troque de sala ou de data";
                    TempData["Erro"] = "Já existe um filme sendo exibido nessa data nessa sala, por favor troque de sala ou de data";

                }
                //ViewData["Erros"] = "Já existe um filme sendo exibido nessa data nessa sala, por favor troque de sala ou de data";

            }
            else
                TempData["Erro"] = "Já existe um filme sendo exibido nessa data nessa sala, por favor troque de sala ou de data";



            return RedirectToAction(nameof(Create));
        }

        // GET: Admin/AdminFilmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmes = await _context.Filmes.Include(f=>f.Categoria).Include(f=>f.Salas).FirstOrDefaultAsync(f => f.Id == id);

            if (filmes == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["SalasId"] = new SelectList(_context.Salas, "Id", "Nome");

            

            return View(filmes);
        }

        // POST: Admin/AdminLanches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile foto,int id, [Bind()] Filmes filmes)
        {
            if (id != filmes.Id)
            {
                return NotFound();
            }

            var categoria = _context.Categorias.FirstOrDefault(l=>l.Id == filmes.Categoria.Id);
            var salas = _context.Salas.FirstOrDefault(l=>l.Id==filmes.Salas.Id);
            filmes.Categoria = categoria;
            filmes.Salas = salas;

            string caminhoParaSalvarImagem = caminhoServidor + "\\imagens\\site\\";
            string novoNome = Guid.NewGuid().ToString() + "_" + foto.FileName;

            if (!Directory.Exists(caminhoParaSalvarImagem))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagem);
            }

            using (var stream = System.IO.File.Create(caminhoParaSalvarImagem + novoNome))
            {
                foto.CopyToAsync(stream);
            }

            filmes.ImagemThumbnailUrl = "/imagens/site/" + novoNome;
            filmes.ImagemUrl = "/imagens/site/" + novoNome;

            try
            {
                _context.Update(filmes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmesExists(filmes.Id))
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

            var filmes= await _context.Filmes
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filmes == null)
            {
                return NotFound();
            }

            return View(filmes);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmes = await _context.Filmes.FindAsync(id);
            filmes.Status = false;
            _context.Filmes.Update(filmes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmesExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
