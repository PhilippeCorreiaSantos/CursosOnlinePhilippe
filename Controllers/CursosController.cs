using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CursosOnlinePhilippe.Models;
using CursosOnlinePhilippe.Data;

namespace CursosOnlinePhilippe.Controllers
{
    public class CursosController : Controller
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Categoria)
                .Include(c => c.Instrutor)
                .ToListAsync();
            return View(cursos);
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Cursos
                .Include(c => c.Categoria)
                .Include(c => c.Instrutor)
                .FirstOrDefaultAsync(c => c.CD_CATEGORIA == id);

            if (curso == null) return NotFound();

            return View(curso);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Instrutores = _context.Instrutores.ToList();
            return View();
        }

        // POST: Cursos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CD_CURSO,NM_CURSO,DS_CURSO,CD_CATEGORIA,CD_INSTRUTOR")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Cursos.Add(curso);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Curso salvo com sucesso: " + curso.NM_CURSO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar curso: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "Erro ao salvar. Tente novamente.");
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
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Instrutores = _context.Instrutores.ToList();
            return View(curso);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Instrutores = _context.Instrutores.ToList();
            return View(curso);
        }

        // POST: Cursos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CD_CURSO,NM_CURSO,DS_CURSO,CD_CATEGORIA,CD_INSTRUTOR")] Curso curso)
        {
            if (id != curso.CD_CATEGORIA) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Cursos.Any(e => e.CD_CATEGORIA == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Instrutores = _context.Instrutores.ToList();
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Cursos
                .Include(c => c.Categoria)
                .Include(c => c.Instrutor)
                .FirstOrDefaultAsync(c => c.CD_CATEGORIA == id);

            if (curso == null) return NotFound();

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
