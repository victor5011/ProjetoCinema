using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCinema.Context;
using ProjetoCinema.Models;

namespace ProjetoCinema.Areas.Admin.Controllers
{
    [Authorize("Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AdminController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = funcionario.UserName,
                    PhoneNumber = funcionario.Telefone.ToString(),
                    PhoneNumberConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, funcionario.Password);


                if (result.Succeeded)
                {
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var func = _context.Funcionarios.FirstOrDefault(f=>f.UserName.Equals(_userManager.GetUserName(User)));

            id = func.Id;

            if (id == null)
            {
                return NotFound();
            }

             func = await _context.Funcionarios.FirstOrDefaultAsync(m => m.Id == id);

            if (func == null)
            {
                return NotFound();
            }

            return View(func);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string senha, [Bind()] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            var funcionarioBusca = _context.Funcionarios.AsNoTracking().FirstOrDefault(c => c.Cpf.Equals(funcionario.Cpf) && !c.UserName.Equals(funcionario.UserName));
            if (funcionarioBusca == null || funcionarioBusca.Id == funcionario.Id)
            {

                try
                {
                    funcionarioBusca = funcionario;
                    _context.Update(funcionarioBusca);
                    await _context.SaveChangesAsync();

                    var usuario = await _userManager.GetUserAsync(User);

                    usuario.UserName = funcionario.UserName;
                    usuario.PhoneNumber = funcionario.Telefone.ToString();
                    usuario.PhoneNumberConfirmed = true;
                    usuario.NormalizedUserName = funcionario.UserName.ToUpper();

                    if (usuario != null)
                    {
                        var resultadoSenha = await _userManager.ChangePasswordAsync(usuario, senha, funcionario.Password);
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
                    if (!FuncionarioExists(funcionario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            return View("Index", funcionarioBusca);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var func = _context.Funcionarios.FirstOrDefault(f => f.UserName.Equals(_userManager.GetUserName(User)));

            id = func.Id;
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(m => m.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            await _userManager.DeleteAsync(user);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
