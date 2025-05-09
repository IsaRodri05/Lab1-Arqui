using Lab1App.Models.Entities;
using Lab1App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab1App.Controllers
{
    public class ProfesionController : Controller
    {
        private readonly IProfesion _repo;

        public ProfesionController(IProfesion repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var profesiones = await _repo.GetAllProfesionesAsync();
            return View(profesiones);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var profesion = await _repo.GetByIdAsync(id);
            if (profesion == null)
                return NotFound();

            return View(profesion);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Profesion profesion)
        {
            if (!ModelState.IsValid)
                return View(profesion);

            await _repo.UpdateAsync(profesion);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var profesion = await _repo.GetByIdAsync(id);
            if (profesion == null)
                return NotFound();

            return View(profesion);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View(new Profesion());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            if (!ModelState.IsValid)
                return View(profesion);

            await _repo.AddAsync(profesion);
            return RedirectToAction(nameof(Index));
        }
    }
}
