using Lab1App.Models.Entities;
namespace Lab1App.Models.Interfaces
{
    public interface IProfesion
    {
        Task<List<Profesion>> GetAllProfesionesAsync();
        Task<Profesion?> GetByIdAsync(int idProf);
        Task AddAsync(Profesion profesion);
        Task UpdateAsync(Profesion profesion);
        Task DeleteAsync(int idProf);
    }
}
