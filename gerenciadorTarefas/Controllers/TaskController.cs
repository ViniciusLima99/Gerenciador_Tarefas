using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gerenciadorTarefas.Models;

namespace gerenciadorTarefas.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Task
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tarefas = await _context.Tarefas.ToListAsync();
            return View(tarefas);
        }

        // GET: /Task/Detalhes/{id}
        [HttpGet("Detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            return tarefa == null ? NotFound() : View(tarefa);
        }

        // GET: /Task/Criar
        [HttpGet("Criar")]
        public IActionResult Criar() => View();

        // POST: /Task/Criar
        [HttpPost("Criar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Tarefas task)
        {
            if (!ModelState.IsValid) return View(task);

            _context.Tarefas.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Task/Editar/{id}
        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            return tarefa == null ? NotFound() : View(tarefa);
        }

        // POST: /Task/Editar/{id}
        [HttpPost("Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Tarefas task)
        {
            if (id != task.Id) return BadRequest();

            if (!ModelState.IsValid) return View(task);

            try
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tarefas.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Task/Excluir/{id}
        [HttpGet("Excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            return tarefa == null ? NotFound() : View(tarefa);
        }

        // POST: /Task/Excluir/{id}
        [HttpPost("Excluir/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmacao(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            if (tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
