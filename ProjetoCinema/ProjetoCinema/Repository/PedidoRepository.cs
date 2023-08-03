using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;

namespace ProjetoCinema.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.carrinhoCompraItems;

            foreach(var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    IngressosId = carrinhoItem.Ingressos.Id,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Ingressos.Preco
                };
                _context.PedidoDetalhes.Add(pedidoDetail);

            }
            _context.SaveChanges();
        }
    }
}
