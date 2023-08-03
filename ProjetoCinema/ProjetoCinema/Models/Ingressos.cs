
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ProjetoCinema.Models
{
    public class Ingressos
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Codigo Cliente")]
        public Cliente Cliente { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O Preço deve estar entre {1 e 999,99}")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public decimal Preco { get; set; }

        public DateTime DataCompra { get; set; }
        public DateTime DataExibicao { get; set; }

        
        [Display(Name = "Filme")]
        public Filmes Filmes { get; set; }
        
        [Display(Name = "Cadeira")]
        public Cadeiras Cadeiras { get; set; }
    }
}
