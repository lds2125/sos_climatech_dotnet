using GsDotNet.DTOs;
using GsDotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GsDotNet.Services
{
    public interface IEventoClimaticoService
    {
        Task<IEnumerable<EventoClimaticoDTO>> GetAllAsync();
        Task<EventoClimaticoDTO> GetByIdAsync(int id);
        Task<EventoClimaticoDTO> CreateAsync(EventoClimaticoDTO eventoDTO);
        Task<EventoClimaticoDTO> UpdateAsync(int id, EventoClimaticoDTO eventoDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EventoClimaticoDTO>> GetByLocalAsync(string local);
        Task<IEnumerable<EventoClimaticoDTO>> GetByTipoAsync(string tipo);
    }
}
