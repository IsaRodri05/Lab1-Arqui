using Lab1App.Models.Interfaces;
using Lab1App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1App.Models.Repositories
{
    public class PersonaRepository: IPersona
    {
        private readonly ArqPerDbContext _context;
        public PersonaRepository(ArqPerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetAllPersonasAsync()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona?> GetByIdAsync(int cc)
        {
            return await _context.Personas.FindAsync(cc);
        }

        public async Task AddAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cc)
        {
            var persona = await GetByIdAsync(cc);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }
    }
}
