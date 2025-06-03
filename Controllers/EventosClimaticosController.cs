using GsDotNet.Models;
using GsDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using GsDotNet.DTOs;
using System.Threading.Tasks;
namespace GsDotNet.Controllers
{
    public class EventosClimaticosController : Controller
    {
        private readonly IEventoClimaticoService _eventoClimaticoService;
        public EventosClimaticosController(IEventoClimaticoService eventoClimaticoService)
        {
            _eventoClimaticoService = eventoClimaticoService;
        }
        public async Task<IActionResult> Index()
        {
            var eventos = await _eventoClimaticoService.GetAllAsync();
            return View(eventos);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var evento = await _eventoClimaticoService.GetByIdAsync(id.Value);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Descricao,DataOcorrencia,Local,Severidade")] EventoClimaticoDTO eventoDto)
        {
            if (ModelState.IsValid)
            {
                await _eventoClimaticoService.CreateAsync(eventoDto);
                return RedirectToAction(nameof(Index));
            }
            return View(eventoDto);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var evento = await _eventoClimaticoService.GetByIdAsync(id.Value);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Descricao,DataOcorrencia,Local,Severidade")] EventoClimaticoDTO eventoDto)
        {
            if (id != eventoDto.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _eventoClimaticoService.UpdateAsync(id, eventoDto);
                }
                catch (Exception) 
                {
                    if (await _eventoClimaticoService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventoDto);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var evento = await _eventoClimaticoService.GetByIdAsync(id.Value);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventoClimaticoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
