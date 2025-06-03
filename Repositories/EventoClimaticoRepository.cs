using GsDotNet.Data;
using GsDotNet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GsDotNet.Repositories
{
    public class EventoClimaticoRepository : IEventoClimaticoRepository
    {
        private readonly ApplicationDbContext _context;
        public EventoClimaticoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EventoClimatico>> GetAllAsync()
        {
            return await _context.GsDotNet
                .Where(e => e.Ativo)
                .OrderByDescending(e => e.DataOcorrencia)
                .ToListAsync();
        }
        public async Task<EventoClimatico> GetByIdAsync(int id)
        {
            return await _context.GsDotNet
                .Include(e => e.PessoasAfetadas)
                .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
        }
        public async Task<EventoClimatico> CreateAsync(EventoClimatico eventoClimatico)
        {
            _context.GsDotNet.Add(eventoClimatico);
            await _context.SaveChangesAsync();
            return eventoClimatico;
        }
        public async Task<EventoClimatico> UpdateAsync(EventoClimatico eventoClimatico)
        {
            _context.Entry(eventoClimatico).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return eventoClimatico;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var evento = await _context.GsDotNet.FindAsync(id);
            if (evento == null)
                return false;
            evento.Ativo = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GsDotNet.AnyAsync(e => e.Id == id && e.Ativo);
        }
        public async Task<IEnumerable<EventoClimatico>> GetByLocalAsync(string local)
        {
            return await _context.GsDotNet
                .Where(e => e.Local.Contains(local) && e.Ativo)
                .OrderByDescending(e => e.DataOcorrencia)
                .ToListAsync();
        }
        public async Task<IEnumerable<EventoClimatico>> GetByTipoAsync(string tipo)
        {
            return await _context.GsDotNet
                .Where(e => e.Tipo.Contains(tipo) && e.Ativo)
                .OrderByDescending(e => e.DataOcorrencia)
                .ToListAsync();
        }
    }
}
