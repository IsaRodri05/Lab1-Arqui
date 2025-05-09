using Lab1App.Models.Entities;

namespace Lab1App.Models.Interfaces
{
    public interface ITelefono
    {
        Task<List<Telefono>> GetAllTelefonosAsync();
        Task<Telefono?> GetByIdAsync(string num);
        Task AddAsync(Telefono telefono);
        Task UpdateAsync(Telefono telefono);
        Task DeleteAsync(string num);
    }
}
