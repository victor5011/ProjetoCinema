using ProjetoCinema.Models;

namespace ProjetoCinema.Repository.Interfaces
{
    public interface IIngressoRepository
    {
        public IEnumerable<Ingressos> Ingressos { get; }
        public Ingressos GetIngressosId(int id);
        public IEnumerable<Ingressos> GetIngressosPorCliente(int id);
        public IEnumerable<Ingressos> GetIngressosPorDataExibicao(DateTime date);
        public IEnumerable<Ingressos> GetIngressosPorDataCompra(DateTime date);
        public IEnumerable<Ingressos> GetIngressosPorFilme(int id);
        public IEnumerable<Ingressos> GetIngressosPorIntervalorDeDataCompra(DateTime date1,DateTime date2);
        public IEnumerable<Ingressos> GetIngressosPorIntervalorDeDataExibicao(DateTime date1, DateTime date2);


        public IEnumerable<Ingressos> GetIngressosPorClienteEDataExibicao(int id,DateTime date);
        public IEnumerable<Ingressos> GetIngressosPorClienteEDataCompra(int id, DateTime date);


        public IEnumerable<Ingressos> GetIngressosPorClientePorDatasCompra(int id, DateTime date1,DateTime date2);
        public IEnumerable<Ingressos> GetIngressosPorClientePorDatasExibicao(int id, DateTime date1, DateTime date2);

    }
}
