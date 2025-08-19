using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CursosOnlinePhilippe.Models;
using CursosOnlinePhilippe.Data;

namespace CursosOnlinePhilippe.Controllers
{
    public class AlunosController : Controller
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            var alunos = await _context.Alunos.ToListAsync();
            return View(alunos);
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(a => a.CD_ALUNO == id);

            if (aluno == null) return NotFound();

            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CD_ALUNO,NM_ALUNO,EML_ALUNO,DT_NASC")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                ModelState.Remove("Avaliacoes");
                ModelState.Remove("Matriculas");
                try
                {
                    _context.Alunos.Add(aluno);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Aluno salvo com sucesso: " + aluno.NM_ALUNO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar aluno: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o aluno. Tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("ModelState inválido:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("- " + error.ErrorMessage);
                }
            }

            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CD_ALUNO,NM_ALUNO,EML_ALUNO,DT_NASC")] Aluno aluno)
        {
            if (id != aluno.CD_ALUNO) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Alunos.Any(e => e.CD_ALUNO == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(a => a.CD_ALUNO == id);

            if (aluno == null) return NotFound();

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
