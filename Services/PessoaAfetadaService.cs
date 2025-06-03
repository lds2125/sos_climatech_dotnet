using GsDotNet.DTOs;
using GsDotNet.Models;
using GsDotNet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GsDotNet.Services
{
    public class PessoaAfetadaService : IPessoaAfetadaService
    {
        private readonly IPessoaAfetadaRepository _repository;
        private readonly IEventoClimaticoRepository _eventoRepository;
        public PessoaAfetadaService(
            IPessoaAfetadaRepository repository,
            IEventoClimaticoRepository eventoRepository)
        {
            _repository = repository;
            _eventoRepository = eventoRepository;
        }
        public async Task<IEnumerable<PessoaAfetadaDTO>> GetAllAsync()
        {
            var pessoas = await _repository.GetAllAsync();
            return pessoas.Select(MapToDTO);
        }
        public async Task<PessoaAfetadaDTO> GetByIdAsync(int id)
        {
            var pessoa = await _repository.GetByIdAsync(id);
            return pessoa != null ? MapToDTO(pessoa) : null;
        }
        public async Task<PessoaAfetadaDTO> CreateAsync(PessoaAfetadaDTO pessoaDTO)
        {
            if (!await _eventoRepository.ExistsAsync(pessoaDTO.EventoClimaticoId))
                throw new Exception("Evento climático não encontrado");
            if (await _repository.ExistsByCpfAsync(pessoaDTO.CPF))
                throw new Exception("CPF já cadastrado no sistema");
            var pessoa = MapToEntity(pessoaDTO);
            pessoa.DataCadastro = DateTime.Now;
            pessoa.Ativo = true;
            var createdPessoa = await _repository.CreateAsync(pessoa);
            return MapToDTO(createdPessoa);
        }
        public async Task<PessoaAfetadaDTO> UpdateAsync(int id, PessoaAfetadaDTO pessoaDTO)
        {
            var existingPessoa = await _repository.GetByIdAsync(id);
            if (existingPessoa == null)
                return null;
            if (!await _eventoRepository.ExistsAsync(pessoaDTO.EventoClimaticoId))
                throw new Exception("Evento climático não encontrado");
            existingPessoa.Nome = pessoaDTO.Nome;
            existingPessoa.DataNascimento = pessoaDTO.DataNascimento;
            existingPessoa.Endereco = pessoaDTO.Endereco;
            existingPessoa.Telefone = pessoaDTO.Telefone;
            existingPessoa.Email = pessoaDTO.Email;
            existingPessoa.TipoAfetacao = pessoaDTO.TipoAfetacao;
            existingPessoa.EventoClimaticoId = pessoaDTO.EventoClimaticoId;
            var updatedPessoa = await _repository.UpdateAsync(existingPessoa);
            return MapToDTO(updatedPessoa);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<IEnumerable<PessoaAfetadaDTO>> GetByEventoIdAsync(int eventoId)
        {
            var pessoas = await _repository.GetByEventoIdAsync(eventoId);
            return pessoas.Select(MapToDTO);
        }
        private PessoaAfetadaDTO MapToDTO(PessoaAfetada pessoa)
        {
            return new PessoaAfetadaDTO
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                CPF = pessoa.CPF,
                DataNascimento = pessoa.DataNascimento,
                Endereco = pessoa.Endereco,
                Telefone = pessoa.Telefone,
                Email = pessoa.Email,
                TipoAfetacao = pessoa.TipoAfetacao,
                EventoClimaticoId = pessoa.EventoClimaticoId,
                NomeEvento = pessoa.EventoClimatico?.Tipo 
            };
        }
        private PessoaAfetada MapToEntity(PessoaAfetadaDTO dto)
        {
            return new PessoaAfetada
            {
                Id = dto.Id,
                Nome = dto.Nome,
                CPF = dto.CPF,
                DataNascimento = dto.DataNascimento,
                Endereco = dto.Endereco,
                Telefone = dto.Telefone,
                Email = dto.Email,
                TipoAfetacao = dto.TipoAfetacao,
                EventoClimaticoId = dto.EventoClimaticoId
            };
        }
    }
}
