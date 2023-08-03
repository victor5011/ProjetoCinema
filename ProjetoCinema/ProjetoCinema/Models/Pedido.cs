using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCinema.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        public Cliente Cliente { get; set; }
        public CarrinhoCompra CarrinhoCompra { get; set; }

        public decimal PedidoTotal { get; set; }
        public decimal TotalItensPedido { get; set; }

        public List<PedidoDetalhe> PedidoItens { get; set; }
    }
}
