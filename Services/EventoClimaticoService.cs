using GsDotNet.DTOs;
using GsDotNet.Models;
using GsDotNet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GsDotNet.Services
{
    public class EventoClimaticoService : IEventoClimaticoService
    {
        private readonly IEventoClimaticoRepository _repository;
        public EventoClimaticoService(IEventoClimaticoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<EventoClimaticoDTO>> GetAllAsync()
        {
            var eventos = await _repository.GetAllAsync();
            return eventos.Select(MapToDTO);
        }
        public async Task<EventoClimaticoDTO> GetByIdAsync(int id)
        {
            var evento = await _repository.GetByIdAsync(id);
            return evento != null ? MapToDTO(evento) : null;
        }
        public async Task<EventoClimaticoDTO> CreateAsync(EventoClimaticoDTO eventoDTO)
        {
            var evento = MapToEntity(eventoDTO);
            evento.DataCadastro = DateTime.Now;
            evento.Ativo = true;
            var createdEvento = await _repository.CreateAsync(evento);
            return MapToDTO(createdEvento);
        }
        public async Task<EventoClimaticoDTO> UpdateAsync(int id, EventoClimaticoDTO eventoDTO)
        {
            var existingEvento = await _repository.GetByIdAsync(id);
            if (existingEvento == null)
                return null;
            existingEvento.Tipo = eventoDTO.Tipo;
            existingEvento.Descricao = eventoDTO.Descricao;
            existingEvento.DataOcorrencia = eventoDTO.DataOcorrencia;
            existingEvento.Local = eventoDTO.Local;
            existingEvento.Severidade = eventoDTO.Severidade;
            var updatedEvento = await _repository.UpdateAsync(existingEvento);
            return MapToDTO(updatedEvento);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<IEnumerable<EventoClimaticoDTO>> GetByLocalAsync(string local)
        {
            var eventos = await _repository.GetByLocalAsync(local);
            return eventos.Select(MapToDTO);
        }
        public async Task<IEnumerable<EventoClimaticoDTO>> GetByTipoAsync(string tipo)
        {
            var eventos = await _repository.GetByTipoAsync(tipo);
            return eventos.Select(MapToDTO);
        }
        private EventoClimaticoDTO MapToDTO(EventoClimatico evento)
        {
            return new EventoClimaticoDTO
            {
                Id = evento.Id,
                Tipo = evento.Tipo,
                Descricao = evento.Descricao,
                DataOcorrencia = evento.DataOcorrencia,
                Local = evento.Local,
                Severidade = evento.Severidade
            };
        }
        private EventoClimatico MapToEntity(EventoClimaticoDTO dto)
        {
            return new EventoClimatico
            {
                Id = dto.Id,
                Tipo = dto.Tipo,
                Descricao = dto.Descricao,
                DataOcorrencia = dto.DataOcorrencia,
                Local = dto.Local,
                Severidade = dto.Severidade
            };
        }
    }
}
