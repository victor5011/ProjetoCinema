using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;

namespace ProjetoCinema.Areas.Member.Controllers
{
    [Authorize("Member")]
    [Area("Member")]
    public class MemberController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public MemberController(UserManager<IdentityUser> userManager,AppDbContext context,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        
        public IActionResult Index()
        {
            var usuario = _userManager.GetUserName(User);
            var cliente = _context.Clientes.FirstOrDefault(c=>c.UserName.Equals(usuario));
            return View(cliente);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);

            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,string senha, [Bind()] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            var clientesBusca = _context.Clientes.AsNoTracking().FirstOrDefault(c=>c.Cpf.Equals(cliente.Cpf) && !c.UserName.Equals(cliente.UserName));
            if(clientesBusca == null || clientesBusca.Id==cliente.Id)
            {
                
                try
                {
                    clientesBusca = cliente;
                    _context.Update(clientesBusca);
                    await _context.SaveChangesAsync();

                    var usuario = await _userManager.GetUserAsync(User);

                    usuario.UserName = cliente.UserName;
                    usuario.PhoneNumber = cliente.Telefone.ToString();
                    usuario.PhoneNumberConfirmed = true;
                    usuario.NormalizedUserName = cliente.UserName.ToUpper();

                    if (usuario != null)
                    {
                        var resultadoSenha = await _userManager.ChangePasswordAsync(usuario, senha, cliente.Password);
                        if (resultadoSenha.Succeeded)
                        {
                            var result = await _userManager.UpdateAsync(usuario);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            
            return View("Index",clientesBusca);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            await _userManager.DeleteAsync(user);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
