using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;
using ProjetoCinema.ViewModels;

namespace ProjetoCinema.Controllers
{
    public class FilmeController : Controller
    {
        private readonly IFilmesRepository _filmesRepository;

        public FilmeController(IFilmesRepository filmesRepository)
        {
            _filmesRepository = filmesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Route="Funcionario")]
        public IActionResult AdicionarFilme()
        {
            return View();
        }

        public IActionResult List(string categoria)
        {
            
            IEnumerable<Filmes> filmes;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(categoria))
            {
                filmes = _filmesRepository.Filmes.OrderBy(f => f.Id);
                categoriaAtual = "Todos os Filmes";
            }
            else
            {
                filmes=_filmesRepository.Filmes.Where(f=>f.Categoria.Nome.Equals(categoria) && f.Status).OrderBy(f=>f.Nome);
                categoriaAtual = categoria;
            }

            var filmelistViewModel = new FilmeListViewModel()
            {
                Filmes= filmes,
                Categoria= categoriaAtual
            };
            return View(filmelistViewModel);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var filme = _filmesRepository.Filmes.FirstOrDefault(f=>f.Id==Id);
            return View(filme);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Filmes> filmes;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(searchString))
            {
                filmes = _filmesRepository.Filmes.OrderBy(p => p.Id);
                categoriaAtual = "Todos os Filmes";
            }
            else
            {
                filmes=_filmesRepository.Filmes.Where(p=>p.Nome.ToLower().Contains(searchString.ToLower()));

                if (filmes.Any())
                    categoriaAtual = "Filmes";
                else
                    categoriaAtual = "Nenhum Filme foi Encontrado";
                
            }
            
            return View("~/Views/Filme/List.cshtml",new FilmeListViewModel
            {
                Filmes=filmes,
                Categoria=categoriaAtual
            });
        }
        
    }
}
