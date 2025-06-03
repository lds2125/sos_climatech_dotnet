using GsDotNet.Models;
using GsDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using GsDotNet.DTOs;
namespace GsDotNet.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasAfetadasApiController : ControllerBase
    {
        private readonly IPessoaAfetadaService _pessoaAfetadaService;
        private readonly IEventoClimaticoService _eventoClimaticoService; 
        public PessoasAfetadasApiController(IPessoaAfetadaService pessoaAfetadaService, IEventoClimaticoService eventoClimaticoService)
        {
            _pessoaAfetadaService = pessoaAfetadaService;
            _eventoClimaticoService = eventoClimaticoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaAfetada>>> GetPessoasAfetadas()
        {
            var pessoas = await _pessoaAfetadaService.GetAllAsync();
            return Ok(pessoas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaAfetada>> GetPessoaAfetada(int id)
        {
            var pessoa = await _pessoaAfetadaService.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }
        [HttpPost]
        public async Task<ActionResult<PessoaAfetada>> PostPessoaAfetada(PessoaAfetadaDTO pessoaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var evento = await _eventoClimaticoService.GetByIdAsync(pessoaDto.EventoClimaticoId);
            if (evento == null)
            {
                ModelState.AddModelError("EventoClimaticoId", "Evento climático não encontrado.");
                return BadRequest(ModelState);
            }
            var pessoa = new PessoaAfetada
            {
                Nome = pessoaDto.Nome,
                CPF = pessoaDto.CPF,
                DataNascimento = pessoaDto.DataNascimento,
                Endereco = pessoaDto.Endereco,
                Telefone = pessoaDto.Telefone,
                Email = pessoaDto.Email,
                TipoAfetacao = pessoaDto.TipoAfetacao,
                EventoClimaticoId = pessoaDto.EventoClimaticoId
            };
            await _pessoaAfetadaService.CreateAsync(pessoaDto);
            return CreatedAtAction(nameof(GetPessoaAfetada), new { id = pessoa.Id }, pessoa);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaAfetada(int id, PessoaAfetadaDTO pessoaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pessoaExistente = await _pessoaAfetadaService.GetByIdAsync(id);
            if (pessoaExistente == null)
            {
                return NotFound();
            }
            var evento = await _eventoClimaticoService.GetByIdAsync(pessoaDto.EventoClimaticoId);
            if (evento == null)
            {
                ModelState.AddModelError("EventoClimaticoId", "Evento climático não encontrado.");
                return BadRequest(ModelState);
            }
            pessoaExistente.Nome = pessoaDto.Nome;
            pessoaExistente.CPF = pessoaDto.CPF;
            pessoaExistente.DataNascimento = pessoaDto.DataNascimento;
            pessoaExistente.Endereco = pessoaDto.Endereco;
            pessoaExistente.Telefone = pessoaDto.Telefone;
            pessoaExistente.Email = pessoaDto.Email;
            pessoaExistente.TipoAfetacao = pessoaDto.TipoAfetacao;
            pessoaExistente.EventoClimaticoId = pessoaDto.EventoClimaticoId;
            await _pessoaAfetadaService.UpdateAsync(id, pessoaDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoaAfetada(int id)
        {
            var pessoa = await _pessoaAfetadaService.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            await _pessoaAfetadaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
