using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;

namespace ProjetoCinema.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> Clientes => _context.Clientes.ToList();

        public Cliente GetClienteId(int id)
        {
            return _context.Clientes.SingleOrDefault(c=>c.Id==id);
        }
    }
}
