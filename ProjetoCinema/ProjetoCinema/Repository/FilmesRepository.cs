using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Repository.Interfaces;
using ProjetoCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCinema.Repository
{
    public class FilmesRepository : IFilmesRepository
    {
        private readonly AppDbContext _context;
        public FilmesRepository(AppDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<Filmes> Filmes => _context.Filmes.Include(c=>c.Categoria);

        public IEnumerable<Filmes> FilmesEmDestaque => _context.Filmes.Where(f => f.FilmeDestaque).Include(c => c.Categoria);

        public Filmes GetByCategoria(int categoriaId)
        {
            //return _context.Filmes.Where(l => l.CategoriaID == categoriaId).Include(l => l.Status).SingleOrDefault();
            return _context.Filmes.SingleOrDefault(l => l.Categoria.Id == categoriaId && l.Status == true);
        }

        public Filmes GetById(int id)
        {
            //return _context.Filmes.Where(l => l.Id == id).Include(l => l.Status).SingleOrDefault();
            return _context.Filmes.SingleOrDefault(l => l.Id == id && l.Status == true);

        }

        public Filmes GetBySalas(int salasId)
        {
           // return _context.Filmes.Where(l => l.SalasID == salasId).Include(l=>l.Status).SingleOrDefault();
           return _context.Filmes.SingleOrDefault(l => l.Salas.Id == salasId && l.Status == true);

        }

        public IEnumerable<Filmes> GetFilmesByDate(DateTime dataInicial, DateTime dataFinal)
        {
            return _context.Filmes.Where(l=>l.DataInicial>=dataInicial && l.DataFinal<=dataFinal && l.Status==true).OrderBy(l=>l.Nome).ToList();
        }
    }
}
