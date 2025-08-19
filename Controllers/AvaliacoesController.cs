using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CursosOnlinePhilippe.Models;
using CursosOnlinePhilippe.Data;

namespace CursosOnlinePhilippe.Controllers
{
    public class AvaliacoesController : Controller
    {
        private readonly AppDbContext _context;

        public AvaliacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Avaliacoes
        public async Task<IActionResult> Index()
        {
            try
            {
                var avaliacoes = await _context.Avaliacoes
                    .Include(a => a.Aluno)
                    .Include(a => a.Curso)
                    .ToListAsync();

                return View(avaliacoes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar avaliações: {ex.Message}");
                return View("Error");
            }
        }

        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var avaliacao = await _context.Avaliacoes
                    .Include(a => a.Aluno)
                    .Include(a => a.Curso)
                    .FirstOrDefaultAsync(a => a.CD_AVALIACAO == id);

                if (avaliacao == null) return NotFound();

                return View(avaliacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar detalhes da avaliação: {ex.Message}");
                return View("Error");
            }
        }

        // GET: Avaliacoes/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.Alunos = _context.Alunos.ToList();
                ViewBag.Cursos = _context.Cursos.ToList();
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar formulário de criação: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Avaliacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CD_AVALIACAO,CD_ALUNO,CD_CURSO,VL_NOTA,DS_COMENTARIO,DT_AVALIACAO")] Avaliacao avaliacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(avaliacao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar avaliação: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao salvar. Tente novamente.");
            }

            ViewBag.Alunos = _context.Alunos.ToList();
            ViewBag.Cursos = _context.Cursos.ToList();
            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var avaliacao = await _context.Avaliacoes.FindAsync(id);
                if (avaliacao == null) return NotFound();

                ViewBag.Alunos = _context.Alunos.ToList();
                ViewBag.Cursos = _context.Cursos.ToList();
                return View(avaliacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar edição: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Avaliacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CD_AVALIACAO,CD_ALUNO,CD_CURSO,VL_NOTA,DS_COMENTARIO,DT_AVALIACAO")] Avaliacao avaliacao)
        {
            if (id != avaliacao.CD_AVALIACAO) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Avaliacoes.Any(e => e.CD_AVALIACAO == id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao editar avaliação: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao editar. Tente novamente.");
            }

            ViewBag.Alunos = _context.Alunos.ToList();
            ViewBag.Cursos = _context.Cursos.ToList();
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var avaliacao = await _context.Avaliacoes
                    .Include(a => a.Aluno)
                    .Include(a => a.Curso)
                    .FirstOrDefaultAsync(a => a.CD_AVALIACAO == id);

                if (avaliacao == null) return NotFound();

                return View(avaliacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar exclusão: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var avaliacao = await _context.Avaliacoes.FindAsync(id);
                if (avaliacao == null) return NotFound();

                _context.Avaliacoes.Remove(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir avaliação: {ex.Message}");
                return View("Error");
            }
        }

        [HttpGet]
        public JsonResult GetCursosPorAluno(int alunoId)
        {
            var cursos = (from m in _context.Matriculas
                        where m.CD_ALUNO == alunoId
                        select new
                        {
                            id = m.Curso.CD_CURSO,
                            nome = m.Curso.NM_CURSO
                        }).Distinct().ToList();

            return Json(cursos);
        }
    }
}
