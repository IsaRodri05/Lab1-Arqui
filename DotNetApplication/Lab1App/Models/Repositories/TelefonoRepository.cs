using Lab1App.Models.Interfaces;
using Lab1App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1App.Models.Repositories
{
    public class TelefonoRepository: ITelefono
    {
        private readonly ArqPerDbContext _context;
        public TelefonoRepository(ArqPerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Telefono>> GetAllTelefonosAsync()
        {
            return await _context.Telefonos.ToListAsync();
        }

        public async Task<Telefono?> GetByIdAsync(int idTel)
        {
            return await _context.Telefonos.FindAsync(idTel);
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

        public async Task DeleteAsync(int idTel)
        {
            var telefono = await GetByIdAsync(idTel);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
