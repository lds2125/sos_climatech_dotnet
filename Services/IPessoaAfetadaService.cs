using GsDotNet.DTOs;
using GsDotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GsDotNet.Services
{
    public interface IPessoaAfetadaService
    {
        Task<IEnumerable<PessoaAfetadaDTO>> GetAllAsync();
        Task<PessoaAfetadaDTO> GetByIdAsync(int id);
        Task<PessoaAfetadaDTO> CreateAsync(PessoaAfetadaDTO pessoaDTO);
        Task<PessoaAfetadaDTO> UpdateAsync(int id, PessoaAfetadaDTO pessoaDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PessoaAfetadaDTO>> GetByEventoIdAsync(int eventoId);
    }
}
