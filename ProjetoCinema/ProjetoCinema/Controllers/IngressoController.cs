using Microsoft.AspNetCore.Mvc;

namespace ProjetoCinema.Controllers
{
    public class IngressoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
