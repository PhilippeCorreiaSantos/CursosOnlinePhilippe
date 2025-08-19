using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CursosOnlinePhilippe.Models;
using CursosOnlinePhilippe.Data;

namespace CursosOnlinePhilippe.Controllers
{
    public class InstrutoresController : Controller
    {
        private readonly AppDbContext _context;

        public InstrutoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Instrutores
        public async Task<IActionResult> Index()
        {
            var instrutores = await _context.Instrutores.ToListAsync();
            return View(instrutores);
        }

        // GET: Instrutores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.CD_INSTRUTOR == id);

            if (instrutor == null) return NotFound();

            return View(instrutor);
        }

        // GET: Instrutores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instrutores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CD_INSTRUTOR,NM_INSTRUTOR,EML_INSTRUTOR,NM_ESPECIALIDADE")] Instrutor instrutor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Instrutores.Add(instrutor);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Instrutor salvo com sucesso: " + instrutor.NM_INSTRUTOR);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar instrutor: " + ex.Message);
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

            return View(instrutor);
        }

        // GET: Instrutores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var instrutor = await _context.Instrutores.FindAsync(id);
            if (instrutor == null) return NotFound();

            return View(instrutor);
        }

        // POST: Instrutores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CD_INSTRUTOR,NM_INSTRUTOR,EML_INSTRUTOR,NM_ESPECIALIDADE")] Instrutor instrutor)
        {
            if (id != instrutor.CD_INSTRUTOR) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instrutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instrutores.Any(e => e.CD_INSTRUTOR == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instrutor);
        }

        // GET: Instrutores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.CD_INSTRUTOR == id);

            if (instrutor == null) return NotFound();

            return View(instrutor);
        }

        // POST: Instrutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instrutor = await _context.Instrutores.FindAsync(id);
            if (instrutor == null) return NotFound();
            _context.Instrutores.Remove(instrutor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
