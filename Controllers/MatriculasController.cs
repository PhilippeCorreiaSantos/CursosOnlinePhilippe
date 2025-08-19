using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CursosOnlinePhilippe.Data;
using CursosOnlinePhilippe.Models;

namespace CursosOnlinePhilippe.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            try
            {
                var matriculas = await _context.Matriculas
                    .Include(m => m.Aluno)
                    .Include(m => m.Curso)
                    .ToListAsync();

                return View(matriculas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar Index: {ex.Message}");
                return View("Error");
            }
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var matricula = await _context.Matriculas
                    .Include(m => m.Aluno)
                    .Include(m => m.Curso)
                    .FirstOrDefaultAsync(m => m.CD_MATRICULA == id);

                if (matricula == null) return NotFound();

                return View(matricula);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar detalhes: {ex.Message}");
                return View("Error");
            }
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            try
            {
                PopularDropdowns();
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar formulário de criação: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Matriculas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Matricula matricula)
        {
            try
            {
                Console.WriteLine($"Aluno: {matricula.CD_ALUNO}, Curso: {matricula.CD_CURSO}, Data: {matricula.DT_MATRICULA}");
                if (ModelState.IsValid)
                {
                    _context.Add(matricula);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                Console.WriteLine("ModelState inválido:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar matrícula: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao salvar. Tente novamente.");
            }

            PopularDropdowns(matricula.CD_ALUNO, matricula.CD_CURSO);
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var matricula = await _context.Matriculas.FindAsync(id);
                if (matricula == null) return NotFound();

                PopularDropdowns(matricula.CD_ALUNO, matricula.CD_CURSO);
                return View(matricula);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar edição: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Matriculas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Matricula matricula)
        {
            if (id != matricula.CD_MATRICULA) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Matriculas.Any(e => e.CD_MATRICULA == id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao editar matrícula: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao editar. Tente novamente.");
            }

            PopularDropdowns(matricula.CD_ALUNO, matricula.CD_CURSO);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var matricula = await _context.Matriculas
                    .Include(m => m.Aluno)
                    .Include(m => m.Curso)
                    .FirstOrDefaultAsync(m => m.CD_MATRICULA == id);

                if (matricula == null) return NotFound();

                return View(matricula);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar exclusão: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var matricula = await _context.Matriculas.FindAsync(id);
                if (matricula == null) return NotFound();

                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir matrícula: {ex.Message}");
                return View("Error");
            }
        }

        // Método auxiliar para popular os dropdowns
        private void PopularDropdowns(int? alunoSelecionado = null, int? cursoSelecionado = null)
        {
            ViewBag.Alunos = new SelectList(_context.Alunos, "CD_ALUNO", "NM_ALUNO", alunoSelecionado);
            ViewBag.Cursos = new SelectList(_context.Cursos, "CD_CURSO", "NM_CURSO", cursoSelecionado);
        }
    }
}
