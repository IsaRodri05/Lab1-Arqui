using Lab1App.Models.Entities;
namespace Lab1App.Models.Interfaces
{
    public interface IPersona
    {
        Task<List<Persona>> GetAllPersonasAsync();
        Task<Persona?> GetByIdAsync(int cc);
        Task AddAsync(Persona persona);
        Task UpdateAsync(Persona persona);
        Task DeleteAsync(int cc);
    }
}
