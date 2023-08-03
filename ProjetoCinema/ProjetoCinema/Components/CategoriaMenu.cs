using Microsoft.AspNetCore.Mvc;
using ProjetoCinema.Repository.Interfaces;

namespace ProjetoCinema.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.categorias.OrderBy(c => c.Nome);
            return View(categorias);
        }
    }
}
