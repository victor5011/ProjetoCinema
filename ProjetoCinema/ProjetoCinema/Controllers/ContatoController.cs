using Microsoft.AspNetCore.Mvc;

namespace ProjetoCinema.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
           return RedirectToAction("Login","Account");
        }
    }
}
