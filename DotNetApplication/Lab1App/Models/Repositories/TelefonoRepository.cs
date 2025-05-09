using Lab1App.Models.Interfaces;
using Lab1App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1App.Models.Repositories
{
    public class TelefonoRepository : ITelefono
    {
        private readonly ArqPerDbContext _context;

        public TelefonoRepository(ArqPerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Telefono>> GetAllTelefonosAsync()
        {
            return await _context.Telefonos
                                 .Include(t => t.DuenioNavigation)
                                 .ToListAsync();
        }

        public async Task<Telefono?> GetByIdAsync(string num)
        {
            return await _context.Telefonos
                                 .Include(t => t.DuenioNavigation)
                                 .FirstOrDefaultAsync(t => t.Num == num);
        }

        public async Task AddAsync(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Telefono telefono)
        {
            _context.Telefonos.Update(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string num)
        {
            var telefono = await GetByIdAsync(num);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
