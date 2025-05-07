using Lab1App.Models.Entities;
namespace Lab1App.Models.Interfaces
{
    public interface IEstudio
    {
        Task<List<Estudio>> GetAllEstudiosAsync();
        Task<Estudio?> GetByIdAsync(int IdProf);
        Task AddAsync(Estudio estudio);
        Task UpdateAsync(Estudio estudio);
        Task DeleteAsync(int IdProf);

    }
}
