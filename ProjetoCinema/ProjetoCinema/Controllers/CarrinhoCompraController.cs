using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinema.Context;
using ProjetoCinema.Models;
using ProjetoCinema.Repository;
using ProjetoCinema.Repository.Interfaces;
using ProjetoCinema.ViewModels;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ProjetoCinema.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly IFilmesRepository _filmesRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IClienteRepository _clienteRepository;
        private readonly ICadeirasRepository _cadeirasRepository;
        private readonly IIngressoRepository _ingressoRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.carrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        public CarrinhoCompraController(IFilmesRepository filmesRepository, CarrinhoCompra carrinhoCompra,IClienteRepository clienteRepository,UserManager<IdentityUser> userManager,ICadeirasRepository cadeirasRepository,AppDbContext context)//, IIngressoRepository ingressoRepository)
        {
            _filmesRepository = filmesRepository;
            _carrinhoCompra = carrinhoCompra;
            _clienteRepository = clienteRepository;
            _userManager = userManager;
            _cadeirasRepository= cadeirasRepository;
            _context = context;
            //_ingressoRepository = ingressoRepository;
        }

        [Authorize]
        public IActionResult AdicionarItemNoCarrinhoCompra(int filmeId, int cadeiraId,FilmeSalasCadeirasViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                
                var userName = _userManager.GetUserName(User);
                var filmeSelecionado = _filmesRepository.Filmes.FirstOrDefault(p => p.Id == filmeId);
                var clienteAtual = _clienteRepository.Clientes.FirstOrDefault(c => c.UserName.Equals(userName));
                var cadeiraAtual = _cadeirasRepository.GetCadeirasId(cadeiraId);

                if (model.dataSelecionada >= filmeSelecionado.DataInicial && model.dataSelecionada <= filmeSelecionado.DataFinal)
                {
                    Ingressos ingresso = new Ingressos()
                    {
                        Filmes = filmeSelecionado,
                        Cliente = clienteAtual,
                        Cadeiras = cadeiraAtual,
                        DataCompra = DateTime.Now,
                        DataExibicao = model.dataSelecionada,
                        Preco = decimal.Parse("25,00")

                    };

                    if (ingresso != null)
                    {
                        _context.Add(ingresso);
                        _context.SaveChanges();
                        _cadeirasRepository.SetCadeiraOcupada(cadeiraId);
                        
                        //ingresso = _context.Ingressos.FirstOrDefault(i => i.Id);
                        _carrinhoCompra.AdicionarAoCarrinho(ingresso);
                    }

                    return RedirectToAction("Index");
                }
            }
            
             return RedirectToAction("Account/Login"); 
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int filmeId)
        {
            //var filmeSelecionado = _filmesRepository.Filmes.FirstOrDefault(p => p.Id == filmeId);
            var ingressoSelecionado=_ingressoRepository.Ingressos.FirstOrDefault(i=>i.Filmes.Id == filmeId);
        
            if(ingressoSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(ingressoSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}
