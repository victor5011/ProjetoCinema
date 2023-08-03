
using ProjetoCinema.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCinema
{
    [Table("CarrinhoCompraItem")]
    public class CarrinhoCompraItem
    {
        public int CarrinhoCompraItemID { get; set; }
        public Ingressos Ingressos { get; set; }        
        public int Quantidade { get; set; }
       
        [StringLength(200)]
        public string CarrinhoCompraID { get; set; }
    }
}
