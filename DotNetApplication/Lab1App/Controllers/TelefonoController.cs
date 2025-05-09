using Lab1App.Models.Entities;
using Lab1App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1App.Controllers
{
    public class TelefonoController : Controller
    {
        private readonly ITelefono _telefonoRepository;
        private readonly IPersona _personaRepository;

        public TelefonoController(ITelefono telefonoRepository, IPersona personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        // GET: /Telefono
        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
            return View(telefonos);
        }

        // GET: /Telefono/Create
        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            var selectList = personas.Select(p => new SelectListItem
            {
                Value = p.Cc.ToString(),
                Text = $"{p.Nombre} {p.Apellido} (Cc: {p.Cc})"
            }).ToList();

            ViewBag.Personas = selectList;
            return View();
        }



        // POST: /Telefono/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            ModelState.Remove("DuenioNavigation");
            if (ModelState.IsValid)
            {
                try
                {
                    await _telefonoRepository.AddAsync(telefono);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar el teléfono: " + ex.Message);
                }
            }

            // Si llegamos aquí, algo falló. Recargamos las personas igual que en el GET
            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewBag.Personas = personas.Select(p => new SelectListItem
            {
                Value = p.Cc.ToString(),
                Text = $"{p.Nombre} {p.Apellido} (Cc: {p.Cc})"
            }).ToList();

            return View(telefono);
        }


        // GET: /Telefono/Edit/num
        public async Task<IActionResult> Edit(string num)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewBag.Personas = new SelectList(personas, "Cc", "Nombre", telefono.Duenio);

            return View(telefono);
        }

        // POST: /Telefono/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string originalNum, Telefono telefono)
        {
            ModelState.Remove("DuenioNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    // Si cambió el número (PK), validar que el nuevo no exista
                    if (telefono.Num != originalNum)
                    {
                        var existing = await _telefonoRepository.GetByIdAsync(telefono.Num);
                        if (existing != null)
                        {
                            ModelState.AddModelError("Num", "Ya existe un teléfono con este número.");
                        }
                        else
                        {
                            // Eliminar el antiguo y crear el nuevo (por ser PK)
                            await _telefonoRepository.DeleteAsync(originalNum);
                            await _telefonoRepository.AddAsync(telefono);
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        await _telefonoRepository.UpdateAsync(telefono);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el teléfono: " + ex.Message);
                }
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewBag.Personas = personas.Select(p => new SelectListItem
            {
                Value = p.Cc.ToString(),
                Text = $"{p.Nombre} {p.Apellido} (Cc: {p.Cc})"
            }).ToList();

            return View(telefono);
        }



        // GET: /Telefono/Delete/num
        public async Task<IActionResult> Delete(string num)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: /Telefono/Delete/num
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string num)
        {
            try
            {
                await _telefonoRepository.DeleteAsync(num);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "No se pudo eliminar el teléfono. " + ex.Message;
                return RedirectToAction(nameof(Delete), new { num });
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
