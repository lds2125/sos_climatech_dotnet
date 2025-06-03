using GsDotNet.Data;
using GsDotNet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GsDotNet.Repositories
{
    public class PessoaAfetadaRepository : IPessoaAfetadaRepository
    {
        private readonly ApplicationDbContext _context;
        public PessoaAfetadaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PessoaAfetada>> GetAllAsync()
        {
            return await _context.PessoasAfetadas
                .Where(p => p.Ativo)
                .Include(p => p.EventoClimatico)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }
        public async Task<PessoaAfetada> GetByIdAsync(int id)
        {
            return await _context.PessoasAfetadas
                .Include(p => p.EventoClimatico)
                .FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }
        public async Task<PessoaAfetada> CreateAsync(PessoaAfetada pessoaAfetada)
        {
            _context.PessoasAfetadas.Add(pessoaAfetada);
            await _context.SaveChangesAsync();
            return pessoaAfetada;
        }
        public async Task<PessoaAfetada> UpdateAsync(PessoaAfetada pessoaAfetada)
        {
            _context.Entry(pessoaAfetada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pessoaAfetada;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var pessoa = await _context.PessoasAfetadas.FindAsync(id);
            if (pessoa == null)
                return false;
            pessoa.Ativo = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PessoasAfetadas.AnyAsync(p => p.Id == id && p.Ativo);
        }
        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.PessoasAfetadas.AnyAsync(p => p.CPF == cpf && p.Ativo);
        }
        public async Task<IEnumerable<PessoaAfetada>> GetByEventoIdAsync(int eventoId)
        {
            return await _context.PessoasAfetadas
                .Where(p => p.EventoClimaticoId == eventoId && p.Ativo)
                .Include(p => p.EventoClimatico)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }
    }
}
