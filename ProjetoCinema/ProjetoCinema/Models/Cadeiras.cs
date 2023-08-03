using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjetoCinema.Models
{
    public class Cadeiras
    {
        [Display(Name = "Codigo da Cadeira")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(5, ErrorMessage = "Nome da Cadeira deve ter no máximo 5 caracteres")]
        [MinLength(1, ErrorMessage = "Nome da Cadeira deve ter no mínimo 1 caracteres")]
        [Display(Name = "Nome da Cadeira")]
        public string Nome { get; set; }

        [Display(Name = "Status Cadeira")]
        public bool Status { get; set; }//verificar disponibilidade
        
        [Display(Name = "Codigo da Sala")]
        public Salas Salas { get; set; }

        
    }
}
