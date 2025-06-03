using GsDotNet.Models;
using GsDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using GsDotNet.DTOs;
namespace GsDotNet.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosClimaticosApiController : ControllerBase
    {
        private readonly IEventoClimaticoService _eventoClimaticoService;
        public EventosClimaticosApiController(IEventoClimaticoService eventoClimaticoService)
        {
            _eventoClimaticoService = eventoClimaticoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoClimatico>>> GetEventosClimaticos()
        {
            var eventos = await _eventoClimaticoService.GetAllAsync();
            return Ok(eventos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoClimatico>> GetEventoClimatico(int id)
        {
            var evento = await _eventoClimaticoService.GetByIdAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return Ok(evento);
        }
        [HttpPost]
        public async Task<ActionResult<EventoClimatico>> PostEventoClimatico(EventoClimaticoDTO eventoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var evento = new EventoClimatico
            {
                Tipo = eventoDto.Tipo,
                Descricao = eventoDto.Descricao,
                DataOcorrencia = eventoDto.DataOcorrencia,
                Local = eventoDto.Local
            };
            await _eventoClimaticoService.CreateAsync(eventoDto);
            return CreatedAtAction(nameof(GetEventoClimatico), new { id = evento.Id }, evento);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventoClimatico(int id, EventoClimaticoDTO eventoDto)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eventoExistente = await _eventoClimaticoService.GetByIdAsync(id);
            if (eventoExistente == null)
            {
                return NotFound();
            }
            eventoExistente.Tipo = eventoDto.Tipo;
            eventoExistente.Descricao = eventoDto.Descricao;
            eventoExistente.DataOcorrencia = eventoDto.DataOcorrencia;
            eventoExistente.Local = eventoDto.Local;
            await _eventoClimaticoService.UpdateAsync(id, eventoDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoClimatico(int id)
        {
            var evento = await _eventoClimaticoService.GetByIdAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            await _eventoClimaticoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
