
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoCinema.Models;

namespace ProjetoCinema.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> categorias {  get; }
    }
}
