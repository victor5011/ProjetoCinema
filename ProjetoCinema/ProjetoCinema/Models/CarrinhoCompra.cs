

using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCinema
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        
        public string CarrinhoCompraID { get; set; }
       
        public List<CarrinhoCompraItem>carrinhoCompraItems { get; set; }

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public static CarrinhoCompra GetCarrinho(IServiceProvider service)
        {
            //define sessão
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo contexto
            var context = service.GetService<AppDbContext>();

            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraID = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Ingressos ingressos)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItems.Include(i=>i.Ingressos).Include(f=>f.Ingressos.Filmes).Where(s=>s.CarrinhoCompraID == CarrinhoCompraID).ToList();
                
            if(carrinhoCompraItem.Count==0)
            {
                var item = new CarrinhoCompraItem
                {
                    CarrinhoCompraID = CarrinhoCompraID,
                    Ingressos = ingressos,
                    Quantidade = 1
                };
                 
                _context.CarrinhoCompraItems.Add(item);
                
            }
            else
            {
                var item = carrinhoCompraItem.FirstOrDefault(i => i.Ingressos.Filmes.Id == ingressos.Filmes.Id && i.Ingressos.DataExibicao==ingressos.DataExibicao);
                
                if(item !=null)
                {
                    item.Quantidade++;
                    _context.Update(item);
                    //_context.CarrinhoCompraItems.Add(item);
                }
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Ingressos ingressos)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
                s => s.Ingressos.Id == ingressos.Id && s.CarrinhoCompraID == CarrinhoCompraID);

            var quantidadeLocal = 0;

            if(carrinhoCompraItem!=null)
            {
                if(carrinhoCompraItem.Quantidade>1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            return carrinhoCompraItems ?? ( carrinhoCompraItems = _context.CarrinhoCompraItems
                .Where(c => c.CarrinhoCompraID == CarrinhoCompraID)
                .Include(s => s.Ingressos).Include(s=>s.Ingressos.Filmes).Include(c=>c.Ingressos.Cliente).ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItems
                .Where(l => l.CarrinhoCompraID == CarrinhoCompraID);

            _context.CarrinhoCompraItems.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total=_context.CarrinhoCompraItems
                .Where(c=>c.CarrinhoCompraID==CarrinhoCompraID)
                .Select(c=>c.Ingressos.Preco * c.Quantidade).Sum();

            return total;
        }
    }
}
