using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;

namespace ProjetoCinema.Repository
{
    public class CadeirasRepository : ICadeirasRepository
    {
        private readonly AppDbContext _context;

        public CadeirasRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cadeiras GetCadeirasId(int id)
        {
            return _context.Cadeiras.SingleOrDefault(c => c.Id == id);
        }

        public void SetCadeiraOcupada(int id)
        {
            var cadeira = _context.Cadeiras.SingleOrDefault(ca => ca.Id == id);
            cadeira.Status = false;
            _context.Update(cadeira);
            _context.SaveChanges();
        }
    }
}
