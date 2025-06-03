using GsDotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GsDotNet.Repositories
{
    public interface IPessoaAfetadaRepository
    {
        Task<IEnumerable<PessoaAfetada>> GetAllAsync();
        Task<PessoaAfetada> GetByIdAsync(int id);
        Task<PessoaAfetada> CreateAsync(PessoaAfetada pessoaAfetada);
        Task<PessoaAfetada> UpdateAsync(PessoaAfetada pessoaAfetada);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<IEnumerable<PessoaAfetada>> GetByEventoIdAsync(int eventoId);
    }
}
