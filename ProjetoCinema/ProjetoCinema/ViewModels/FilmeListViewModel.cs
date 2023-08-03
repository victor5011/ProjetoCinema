using ProjetoCinema.Models;

namespace ProjetoCinema.ViewModels
{
    public class FilmeListViewModel
    {
        public IEnumerable<Filmes> Filmes { get; set; }
        public string Categoria { get; set; }
    }
}
