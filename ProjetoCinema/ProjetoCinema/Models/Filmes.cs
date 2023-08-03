
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinema.Models
{
    [Table("Filme")]
    public class Filmes
    {
        [Display(Name = "Codigo")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(150, ErrorMessage = "Nome do Filme deve ter no máximo 150 caracteres")]
        [MinLength(1, ErrorMessage = "Nome do Filme deve ter no mínimo 1 caracteres")]
        [Display(Name = "Nome do Filme")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(500, ErrorMessage = "Sinopse deve ter no máximo 500 caracteres")]
        [MinLength(1, ErrorMessage = "Nome do Filme deve ter no mínimo 1 caracteres")]
        [Display(Name = "Sinopse do Filme")]
        public string Sinopse { get; set; }
        [Display(Name = "Filme em Exibição ?")]
        public bool Status { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Imagem de Cartaz")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Imagem de Exibição")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Filme em Destaque ")]
        public bool FilmeDestaque { get; set; }
       
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Duracao { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Data de Inicio da Exibicao")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime DataInicial { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Data de Encerramento da Exibicao")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime DataFinal { get; set; }

        public Salas Salas { get; set; }
        public  Categoria Categoria { get; set; }
    }
}
