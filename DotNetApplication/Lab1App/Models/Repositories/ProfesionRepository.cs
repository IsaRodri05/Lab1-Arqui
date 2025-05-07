using Lab1App.Models.Interfaces;
using Lab1App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1App.Models.Repositories
{
    public class ProfesionRepository: IProfesion
    {
        private readonly ArqPerDbContext _context;
        public ProfesionRepository(ArqPerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Profesion>> GetAllProfesionesAsync()
        {
            return await _context.Profesions.ToListAsync();
        }

        public async Task<Profesion?> GetByIdAsync(int idProf)
        {
            return await _context.Profesions.FindAsync(idProf);
        }

        public async Task AddAsync(Profesion profesion)
        {
            _context.Profesions.Add(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profesion profesion)
        {
            _context.Profesions.Update(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idProf)
        {
            var profesion = await GetByIdAsync(idProf);
            if (profesion != null)
            {
                _context.Profesions.Remove(profesion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
