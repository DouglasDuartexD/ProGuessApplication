using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProGuessApplication.Data;
using ProGuessApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ProGuessApplication.Controllers
{
    [Authorize]
    public class gitController : Controller
    {
        private readonly ProGuessApplicationContext _context;

        public gitController(ProGuessApplicationContext context)
        {
            _context = context;
        }

        // GET: git

        // GET: git/Details/5


        // GET: git/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: git/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("id,nome_usuario,usuario_id,key")] git git)
        {
            if (ModelState.IsValid)
            {
                var _UsuarioContext = _context.usuario.Where(m => m.email == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
                git.usuario_id = _UsuarioContext.id;
                _context.Add(git);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(git);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.git == null)
            {
                return NotFound();
            }

            var git = await _context.git
                .FirstOrDefaultAsync(m => m.id == id);
            if (git == null)
            {
                return NotFound();
            }

            return View(git);
        }
        // GET: git/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.git == null)
            {
                return NotFound();
            }

            var git = await _context.git.FindAsync(id);
            if (git == null)
            {
                return NotFound();
            }
            return View(git);
        }

        // POST: git/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,email,usuario_id,key")] git git)
        {
            if (id != git.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(git);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!gitExists(git.id))
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
            return View(git);
        }

        // GET: git/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.git == null)
            {
                return NotFound();
            }

            var git = await _context.git
                .FirstOrDefaultAsync(m => m.id == id);
            if (git == null)
            {
                return NotFound();
            }

            return View(git);
        }

        // POST: git/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.git == null)
            {
                return Problem("Entity set 'ProGuessApplicationContext.git'  is null.");
            }
            var git = await _context.git.FindAsync(id);
            if (git != null)
            {
                _context.git.Remove(git);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool gitExists(int id)
        {
          return _context.git.Any(e => e.id == id);
        }
    }
}