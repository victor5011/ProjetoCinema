using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoCinema.Context;
using ProjetoCinema.Repository.Interfaces;
using ProjetoCinema.Models;

namespace ProjetoCinema.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> categorias => _context.Categorias.OrderBy(c=>c.Nome);
    }
}
