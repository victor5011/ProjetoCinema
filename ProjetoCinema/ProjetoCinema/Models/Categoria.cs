
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjetoCinema.Models
{
    public class Categoria
    {
        [Display(Name = "Codigo da Categoria")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(50, ErrorMessage = "Nome da Categoria deve ter no máximo 50 caracteres")]
        [MinLength(1, ErrorMessage = "Nome da Categoria deve ter no mínimo 1 caracteres")]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; }

    }
}
