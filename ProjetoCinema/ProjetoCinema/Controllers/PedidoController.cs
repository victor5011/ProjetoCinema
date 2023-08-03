using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinema.Models;
using ProjetoCinema.Repository.Interfaces;

namespace ProjetoCinema.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IClienteRepository _clienteRepository;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra,IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
            _clienteRepository = clienteRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(string carrinhoId)
        {
            var lista = _carrinhoCompra.GetCarrinhoCompraItems();
            var Carrinho = lista.FirstOrDefault(l => l.CarrinhoCompraID.Equals(carrinhoId));

            var pedido = new Pedido()
            {
                Cliente = Carrinho.Ingressos.Cliente
            };
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItems();  
            _carrinhoCompra.carrinhoCompraItems = items;

            if(_carrinhoCompra.carrinhoCompraItems.Count == 0) 
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um Filme. . .");
            }

            foreach(var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Ingressos.Preco * item.Quantidade);
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            if(ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido=_carrinhoCompra.GetCarrinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();

                return View("~/Views/Pedido/CheckoutCompleto.cshtml",pedido);
            }
            return View(pedido); 
        }

    }
}
