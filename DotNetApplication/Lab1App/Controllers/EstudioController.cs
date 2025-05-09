using Lab1App.Models.Interfaces;
using Lab1App.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1App.Controllers
{
    public class EstudioController : Controller
    {
        private readonly IEstudio _estudioRepository;

        public EstudioController(IEstudio estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();

            // Asegúrate de que las propiedades de navegación estén cargadas.
            // Si las relaciones están configuradas como "Lazy Loading" esto no es necesario.
            // Pero para estar seguros:
            foreach (var estudio in estudios)
            {
                // Evita nulos en la vista
                _ = estudio.CcPerNavigation ??= new();
                _ = estudio.IdProfNavigation ??= new();
            }

            return View(estudios);
        }

        public async Task<IActionResult> Delete(int idProf)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf);
            if (estudio == null)
                return NotFound();

            return View(estudio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf)
        {
            try
            {
                await _estudioRepository.DeleteAsync(idProf);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "No se pudo eliminar el estudio. " + ex.Message;
                return RedirectToAction(nameof(Delete), new { idProf });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
