using Microsoft.AspNetCore.Mvc;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;
using ProjetoCinema.ViewModels;
using System.Diagnostics;

namespace ProjetoCinema.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IFilmesRepository _filmesRepository;

        public HomeController(IFilmesRepository filmesRepository)
        {
            _filmesRepository = filmesRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                FilmesDestaques = _filmesRepository.FilmesEmDestaque
            };
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}