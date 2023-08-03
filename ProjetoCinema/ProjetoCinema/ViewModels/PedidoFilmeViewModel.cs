using ProjetoCinema.Models;

namespace ProjetoCinema.ViewModels
{
    public class PedidoFilmeViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }   
    }
}
