using ProjetoCinema.Models;

namespace ProjetoCinema.Repository.Interfaces
{
    public interface IFilmesRepository
    {
        IEnumerable<Filmes> Filmes { get; }
        Filmes GetById(int id);
        Filmes GetByCategoria(int categoriaId);
        Filmes GetBySalas(int salasId);
        IEnumerable<Filmes> GetFilmesByDate(DateTime dataInicial, DateTime dataFinal);
        public IEnumerable<Filmes> FilmesEmDestaque { get; }
    }
}
