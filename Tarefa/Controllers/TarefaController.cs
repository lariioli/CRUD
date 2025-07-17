using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarefaApp.Models;

namespace Tarefa.Controllers
{
    public class TarefaController : Controller
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tarefa
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarefa.ToListAsync());
        }

        // GET: Tarefa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tarefa = await _context.Tarefa.FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null) return NotFound();

            return View(tarefa);
        }

        // GET: Tarefa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Concluida")] TarefaApp.Models.Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null) return NotFound();

            return View(tarefa);
        }

        // POST: Tarefa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Concluida")] TarefaApp.Models.Tarefa tarefa)
        {
            if (id != tarefa.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tarefa.Any(e => e.Id == tarefa.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tarefa = await _context.Tarefa.FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null) return NotFound();

            return View(tarefa);
        }

        // POST: Tarefa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefa.Remove(tarefa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
