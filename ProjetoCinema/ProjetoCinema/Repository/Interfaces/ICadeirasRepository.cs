using ProjetoCinema.Models;

namespace ProjetoCinema.Repository.Interfaces
{
    public interface ICadeirasRepository
    {
        Cadeiras GetCadeirasId(int id);
        void SetCadeiraOcupada(int id);
    }
}
