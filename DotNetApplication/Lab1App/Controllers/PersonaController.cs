using Lab1App.Models.Entities;
using Lab1App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab1App.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersona _personaRepository;

        public PersonaController(IPersona personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // GET: /Persona
        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return View(personas);
        }

        // GET: /Persona/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Persona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            // Verifica si la cédula ya existe
            var existingPersona = await _personaRepository.GetByIdAsync(persona.Cc);
            if (existingPersona != null)
            {
                ModelState.AddModelError("Cc", "Ya existe una persona con esta cédula.");
            }

            if (ModelState.IsValid)
            {
                await _personaRepository.AddAsync(persona);
                return RedirectToAction(nameof(Index));
            }

            // Si llega aquí, significa que algo falló
            return View(persona);
        }

        // GET: /Persona/Edit/5
        public async Task<IActionResult> Edit(int cc)
        {
            var persona = await _personaRepository.GetByIdAsync(cc);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: /Persona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int cc, Persona persona)
        {
            if (cc != persona.Cc)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _personaRepository.UpdateAsync(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: /Persona/Delete/5
        public async Task<IActionResult> Delete(int cc)
        {
            var persona = await _personaRepository.GetByIdAsync(cc);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: /Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int cc)
        {
            var persona = await _personaRepository.GetByIdAsync(cc);
            if (persona == null)
            {
                return NotFound();
            }

            // Llamada al repositorio para eliminar la persona
            await _personaRepository.DeleteAsync(cc);

            // Redirigir a la lista de personas después de eliminar
            return RedirectToAction(nameof(Index));
        }
    }
}
