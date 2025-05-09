using Lab1App.Models.Interfaces;
using Lab1App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1App.Models.Repositories
{
    public class EstudioRepository: IEstudio
    {
        private readonly ArqPerDbContext _context;
        public EstudioRepository(ArqPerDbContext context)
        {
            _context = context;
        }


        public async Task<List<Estudio>> GetAllEstudiosAsync()
        {
            return await _context.Estudios
                .Include(e => e.IdProfNavigation)
                .Include(e => e.CcPerNavigation)
                .ToListAsync();
        }


        public async Task<Estudio?> GetByIdAsync(int idProf)
        {
            return await _context.Estudios
                .Include(e => e.IdProfNavigation)
                .Include(e => e.CcPerNavigation)
                .FirstOrDefaultAsync(e => e.IdProf == idProf);
        }


        public async Task AddAsync(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int idProf)
        {
            var estudio = await GetByIdAsync(idProf);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
