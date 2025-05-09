using Lab1App.Models.Entities;
using Lab1App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1App.Controllers
{
    public class EstudioController : Controller
    {
        private readonly IEstudio _estudioRepository;
        private readonly IPersona _personaRepository;
        private readonly IProfesion _profesionRepository;

        public EstudioController(IEstudio estudioRepository, IPersona personaRepository, IProfesion profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();
            return View(estudios);
        }

        // GET: /Estudio/Create
        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();

            ViewBag.Personas = personas.Select(p => new SelectListItem
            {
                Value = p.Cc.ToString(),
                Text = $"{p.Nombre} {p.Apellido} (Cc: {p.Cc})"
            }).ToList();

            ViewBag.Profesiones = profesiones.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nom
            }).ToList();

            return View();
        }

        // POST: /Estudio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    await _estudioRepository.AddAsync(estudio);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar el estudio: " + ex.Message);
                }
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();

            ViewBag.Personas = personas.Select(p => new SelectListItem
            {
                Value = p.Cc.ToString(),
                Text = $"{p.Nombre} {p.Apellido} (Cc: {p.Cc})"
            }).ToList();

            ViewBag.Profesiones = profesiones.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nom
            }).ToList();

            return View(estudio);
        }

        // GET: /Estudio/Edit/5/5
        public async Task<IActionResult> Edit(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdsAsync(idProf, ccPer);
            if (estudio == null)
                return NotFound();

            await CargarListasDesplegables();
            ViewBag.IdProfOriginal = idProf;
            ViewBag.CcPerOriginal = ccPer;

            return View(estudio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProfOriginal, int ccPerOriginal, Estudio estudio)
        {
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            if (!ModelState.IsValid)
            {
                await CargarListasDesplegables();
                return View(estudio);
            }

            try
            {
                // Si cambió la clave primaria
                if (idProfOriginal != estudio.IdProf || ccPerOriginal != estudio.CcPer)
                {
                    // Verificar si ya existe el nuevo estudio
                    var existe = await _estudioRepository.GetByIdsAsync(estudio.IdProf, estudio.CcPer);
                    if (existe != null)
                    {
                        ModelState.AddModelError("", "Ya existe un estudio con esta combinación de Persona y Profesión");
                        await CargarListasDesplegables();
                        return View(estudio);
                    }

                    // Eliminar el original y crear el nuevo
                    await _estudioRepository.DeleteAsync(idProfOriginal, ccPerOriginal);
                    await _estudioRepository.AddAsync(estudio);
                }
                else
                {
                    // Actualizar normalmente
                    await _estudioRepository.UpdateAsync(estudio);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                await CargarListasDesplegables();
                return View(estudio);
            }
        }

        private async Task CargarListasDesplegables()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();

            ViewBag.Personas = personas.Select(p => new SelectListItem
            {
                Value = p.Cc.ToString(),
                Text = $"{p.Nombre} {p.Apellido} (CC: {p.Cc})"
            }).ToList();

            ViewBag.Profesiones = profesiones.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nom
            }).ToList();
        }

        // GET: /Estudio/Delete/5/5
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdsAsync(idProf, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }

            // Cargar datos de navegación para mostrar en la vista
            estudio.IdProfNavigation = await _profesionRepository.GetByIdAsync(estudio.IdProf);
            estudio.CcPerNavigation = await _personaRepository.GetByIdAsync(estudio.CcPer);

            return View(estudio);
        }

        // POST: /Estudio/Delete/5/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            try
            {
                await _estudioRepository.DeleteAsync(idProf, ccPer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el estudio: " + ex.Message);

                // Recargar el estudio para mostrarlo nuevamente
                var estudio = await _estudioRepository.GetByIdsAsync(idProf, ccPer);
                estudio.IdProfNavigation = await _profesionRepository.GetByIdAsync(idProf);
                estudio.CcPerNavigation = await _personaRepository.GetByIdAsync(ccPer);

                return View("Delete", estudio);
            }
        }

    }
}
