using GsDotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GsDotNet.Repositories
{
    public interface IEventoClimaticoRepository
    {
        Task<IEnumerable<EventoClimatico>> GetAllAsync();
        Task<EventoClimatico> GetByIdAsync(int id);
        Task<EventoClimatico> CreateAsync(EventoClimatico eventoClimatico);
        Task<EventoClimatico> UpdateAsync(EventoClimatico eventoClimatico);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<EventoClimatico>> GetByLocalAsync(string local);
        Task<IEnumerable<EventoClimatico>> GetByTipoAsync(string tipo);
    }
}
