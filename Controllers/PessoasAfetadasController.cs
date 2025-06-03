using GsDotNet.Models;
using GsDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using GsDotNet.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;
namespace GsDotNet.Controllers
{
    public class PessoasAfetadasController : Controller
    {
        private readonly IPessoaAfetadaService _pessoaAfetadaService;
        private readonly IEventoClimaticoService _eventoClimaticoService;
        public PessoasAfetadasController(IPessoaAfetadaService pessoaAfetadaService, IEventoClimaticoService eventoClimaticoService)
        {
            _pessoaAfetadaService = pessoaAfetadaService;
            _eventoClimaticoService = eventoClimaticoService;
        }
        public async Task<IActionResult> Index()
        {
            var pessoas = await _pessoaAfetadaService.GetAllAsync();
            return View(pessoas);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pessoa = await _pessoaAfetadaService.GetByIdAsync(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }
        public async Task<IActionResult> Create()
        {
            await PopulateEventosDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CPF,DataNascimento,Endereco,Telefone,Email,TipoAfetacao,EventoClimaticoId")] PessoaAfetadaDTO pessoaDto)
        {
            ModelState.Remove(nameof(pessoaDto.NomeEvento)); 
            if (ModelState.IsValid)
            {
                try
                {
                    await _pessoaAfetadaService.CreateAsync(pessoaDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao criar pessoa: {ex.Message}");
                }
            }
            await PopulateEventosDropDownList(pessoaDto.EventoClimaticoId);
            return View(pessoaDto);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pessoa = await _pessoaAfetadaService.GetByIdAsync(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }
            await PopulateEventosDropDownList(pessoa.EventoClimaticoId);
            return View(pessoa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,DataNascimento,Endereco,Telefone,Email,TipoAfetacao,EventoClimaticoId")] PessoaAfetadaDTO pessoaDto)
        {
            if (id != pessoaDto.Id)
            {
                return NotFound();
            }
            ModelState.Remove(nameof(pessoaDto.NomeEvento)); 
            if (ModelState.IsValid)
            {
                try
                {
                    await _pessoaAfetadaService.UpdateAsync(id, pessoaDto);
                    return RedirectToAction(nameof(Index));
                }
                 catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao atualizar pessoa: {ex.Message}");
                    if (await _pessoaAfetadaService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                }
            }
            await PopulateEventosDropDownList(pessoaDto.EventoClimaticoId);
            return View(pessoaDto);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pessoa = await _pessoaAfetadaService.GetByIdAsync(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pessoaAfetadaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateEventosDropDownList(object selectedEvento = null)
        {
            var eventos = await _eventoClimaticoService.GetAllAsync();
            ViewBag.EventoClimaticoId = new SelectList(eventos.OrderBy(e => e.Tipo), "Id", "Tipo", selectedEvento);
        }
    }
}
