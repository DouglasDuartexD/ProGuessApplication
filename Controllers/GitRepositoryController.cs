using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProGuessApplication.Data;
using ProGuessApplication.Models;
using System.Security.Claims;
using System.Text.Json.Nodes;
using X.PagedList;
using X.PagedList.Mvc;
using System;
using Newtonsoft.Json;

namespace ProGuessApplication.Controllers
{
    public class GitRepositoryController : Controller
    {
        private readonly ProGuessApplicationContext _context;
        public GitRepositoryController(ProGuessApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetRepository()
        {
            List<Root> Repositorios = new List<Root>();
            List<Root> Repositorio = new List<Root>();
        
            var _UsuarioContext = _context.usuario.Where(
                m => m.email == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();

            var _GitContext = _context.git.Where(m => m.usuario_id == _UsuarioContext.id).ToList();
            foreach (var git in _GitContext)
            {
                if (git.nome_usuario != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.github.com/users/" + git.nome_usuario + "/repos"))
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
            return View(Repositorios);

        }
    }
}
            

