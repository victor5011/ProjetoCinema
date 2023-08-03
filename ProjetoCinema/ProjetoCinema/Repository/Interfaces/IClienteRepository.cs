using ProjetoCinema.Models;

namespace ProjetoCinema.Repository.Interfaces
{
    public interface IClienteRepository
    {
        public IEnumerable<Cliente> Clientes { get; }
        public Cliente GetClienteId(int id);
        
    }
}
