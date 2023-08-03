
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjetoCinema.Models
{
    public class Funcionario
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(150, ErrorMessage = "Nome do Cliente deve ter no máximo 150 caracteres")]
        [MinLength(1, ErrorMessage = "Nome do Cliente deve ter no mínimo 1 caracteres")]
        [Display(Name = "Nome do Cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(150, ErrorMessage = "Endereço deve ter no máximo 150 caracteres")]
        [MinLength(1, ErrorMessage = "Endereço deve ter no mínimo 1 caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(150, ErrorMessage = "Bairro deve ter no máximo 150 caracteres")]
        [MinLength(1, ErrorMessage = "Bairro deve ter no mínimo 1 caracteres")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(150, ErrorMessage = "Cidade deve ter no máximo 150 caracteres")]
        [MinLength(1, ErrorMessage = "Cidade deve ter no mínimo 1 caracteres")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(11, ErrorMessage = "CPF deve ter no máximo 11 caracteres")]
        [MinLength(11, ErrorMessage = "CPF deve ter no mínimo 11 caracteres")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Digite um Login!")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Telefone")]
        public double Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        
    }
}
