using ProjetoCinema.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCinema.ViewModels
{
    public class FilmeSalasCadeirasViewModel
    {
        public Filmes filmes { get; set; }
        public IEnumerable<Cadeiras> cadeiras { get; set; }
        public Salas salas { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime dataSelecionada { get; set; }
    }
}
