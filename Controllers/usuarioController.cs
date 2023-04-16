using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using ProGuessApplication.Data;
using ProGuessApplication.Models;
using System.Security.Claims;
using System.Text.Json.Nodes;
using X.PagedList;
using X.PagedList.Mvc;
using System;
namespace ProGuessApplication.Controllers
    
{

    
    public class usuarioController : Controller
    {

        private readonly ProGuessApplicationContext _context;

        public usuarioController(ProGuessApplicationContext context)
        {
            _context = context;
        }



        // Index do usuário, exibindo os repositórios cadastrados do github no banco.
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            List<Root> Repositorios = new List<Root>();
            List<Root> Repositorio = new List<Root>();
            ViewBag.CurrentSort = sortOrder;

            var _UsuarioContext = _context.usuario.Where(
                m => m.email == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();

            var _GitContext = _context.git.Where(m => m.usuario_id == _UsuarioContext.id).ToList();
          foreach (var git in _GitContext) 
           {
              if (git.nome_usuario != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.github.com/users/"+ git.nome_usuario+"/repos"))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github+json");
                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer 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");
                        request.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");
                        request.Headers.TryAddWithoutValidation("User-Agent", "douglasduartexd");

                        var _response = await httpClient.SendAsync(request);
                        if (_response.IsSuccessStatusCode)
                        {

                                var RepResponse = _response.Content.ReadAsStringAsync().Result;

                                //Deserializa um array json em um objeto do modelo Root
                                Repositorio = JsonConvert.DeserializeObject<List<Root>>(RepResponse);
                                //Adiciona todos os resultados do repositório em um outra variavel modelo para comportar todos os resultados
                                //dos repositórios cadastrados
                                foreach (var item in Repositorio)
                                {
                                    item.repositorio_id = git.id;
                                    item.DataCriacao = item.created_at;
                                    item.HoraCriacao = item.created_at;
                                    item.DataUltimoUpdate = item.updated_at;
                                    item.HoraUltimoUpdate = item.updated_at;
                                    Repositorios.Add(item);

                                }
                            }
                        else
                        {
                            
                        }

                    }
                }
              }
                
            }

          //Verifica se a caixa de pesquisa para busca dos dados
            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
                Repositorios = Repositorios.Where(
                   s => s.name.Contains(searchString) 
                || s.owner.name.Contains(searchString)
                ).ToList();
            }
            else if(!String.IsNullOrEmpty(currentFilter))
            {

                searchString = currentFilter;
                Repositorios = Repositorios.Where(
   s => s.name.Contains(searchString)
|| s.owner.name.Contains(searchString)
).ToList();

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.Page = page;
            ViewBag.CurrentFilter = searchString;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(Repositorios.ToPagedList(pageNumber,pageSize));
        }

        // GET: usuario2/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario
                .FirstOrDefaultAsync(m => m.id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: usuario2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usuario2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,email,senha,perfil,perfilId,deletado,desativado,telefone")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.deletado = 0;
                usuario.desativado = 0;
                usuario.perfil = "Padrão";
                usuario.perfilId = 1;
                usuario.telefone = "";
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: usuario2/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: usuario2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,email,senha,perfil,perfilId,deletado,desativado,telefone")] usuario usuario)
        {
            if (id != usuario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usuarioExists(usuario.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: usuario2/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario
                .FirstOrDefaultAsync(m => m.id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: usuario2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuario == null)
            {
                return Problem("Entity set 'ProGuessApplicationContext.usuario'  is null.");
            }
            var usuario = await _context.usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.usuario.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool usuarioExists(int id)
        {
            return _context.usuario.Any(e => e.id == id);
        }
    }
}
