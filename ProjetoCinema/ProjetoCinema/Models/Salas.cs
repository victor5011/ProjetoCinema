
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace ProjetoCinema.Models
{
    public class Salas
    {
        [Display(Name = "Codigo da Sala")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(10, ErrorMessage = "Nome da Sala deve ter no máximo 10 caracteres")]
        [MinLength(5,ErrorMessage = "Nome da Sala deve ter no mínimo 5 caracteres")]
        [Display(Name = "Nome da Sala")]
        public string Nome { get; set; }

        
    }
}
