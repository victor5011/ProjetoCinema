using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;

namespace ProjetoCinema.Repository
{
    public class IngressoRepository : IIngressoRepository
    {
        private readonly AppDbContext _context;

        public IngressoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ingressos> Ingressos => _context.Ingressos.Include(c=>c.Cadeiras).Include(d=>d.Cliente);

        public Ingressos GetIngressosId(int id)
        {
            return _context.Ingressos.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Ingressos> GetIngressosPorCliente(int id)
        {
            return _context.Ingressos.Where(c => c.Cliente.Id == id).ToList();
        }

        public IEnumerable<Ingressos> GetIngressosPorClienteEDataCompra(int id, DateTime date)
        {
            return _context.Ingressos.Where(c => c.Cliente.Id == id && c.DataCompra == date).ToList();

        }

        public IEnumerable<Ingressos> GetIngressosPorClientePorDatasCompra(int id, DateTime date1, DateTime date2)
        {
            return _context.Ingressos.Where(c => c.Cliente.Id == id && c.DataCompra >= date1 && c.DataCompra <= date2).ToList();

        }

        public IEnumerable<Ingressos> GetIngressosPorClienteEDataExibicao(int id, DateTime date)
        {
            return _context.Ingressos.Where(c => c.Cliente.Id == id && c.DataExibicao == date).ToList();

        }
        public IEnumerable<Ingressos> GetIngressosPorClientePorDatasExibicao(int id, DateTime date1, DateTime date2)
        {
            return _context.Ingressos.Where(c => c.Cliente.Id == id && c.DataExibicao >= date1 && c.DataExibicao <= date2).ToList();
        }

        public IEnumerable<Ingressos> GetIngressosPorDataCompra(DateTime date)
        {
            return _context.Ingressos.Where(c => c.DataCompra >= date).ToList();
        }

        public IEnumerable<Ingressos> GetIngressosPorDataExibicao(DateTime date)
        {
            return _context.Ingressos.Where(c => c.DataExibicao >= date).ToList();
        }

        public IEnumerable<Ingressos> GetIngressosPorFilme(int id)
        {
            return _context.Ingressos.Where(c => c.Filmes.Id==id).ToList();
        }

        public IEnumerable<Ingressos> GetIngressosPorIntervalorDeDataCompra(DateTime date1, DateTime date2)
        {
            return _context.Ingressos.Where(c => c.DataExibicao >= date1 && c.DataExibicao <= date2).ToList();

        }
        public IEnumerable<Ingressos> GetIngressosPorIntervalorDeDataExibicao(DateTime date1, DateTime date2)
        {
            return _context.Ingressos.Where(c => c.DataExibicao >= date1 && c.DataExibicao <= date2).ToList();

        }
    }
}
