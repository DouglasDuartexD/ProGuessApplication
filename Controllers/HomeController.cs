using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProGuessApplication.Models;
using System.Diagnostics;
using System.Security.Claims;
using ProGuessApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Identity;

namespace ProGuessApplication.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ProGuessApplicationContext _context;

        public HomeController(ProGuessApplicationContext context)
        {
            _context = context;
        }



        public IActionResult index()
        {
            var UsuarioLogado = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsuarioLogado != null) {
                return RedirectToAction("Index", "usuario");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            usuario _UsuarioContext = new usuario();

            var status = _context.usuario.Where(m => m.email == modelLogin.email && m.senha == modelLogin.senha).FirstOrDefault();

            if (status != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.email)
                   // new Claim("OtherProperties","Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = modelLogin.keepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "usuario");
            }



            ViewData["ValidateMessage"] = "user not found";
            return RedirectToAction("Index", "Home");

        }
    }
}