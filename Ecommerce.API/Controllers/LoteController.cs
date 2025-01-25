using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.Contexts;
using Ecommerce.Models;
using Ecommerce.Interfaces;
using Ecommerce.Models.DTOs;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LoteController(ILoteService loteService)
        {
            _loteService = loteService;
        }

        // GET: api/Lote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lote>>> GetLotes(Guid id)
        {
            var response = await _loteService.GetLotesPorProducto(id);
            return Ok(response);
        }

        //GET: api/Lote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lote>> GetLote(Guid id)
        {
            var lote = await _loteService.GetLoteById(id);

            if (lote == null)
            {
                return NotFound();
            }

            return lote;
        }

        // PUT: api/Lote/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLote(Guid id, int cantidad, bool activo)
        {
            if (id == null)
            {
                return NotFound("No se encontró el id");
            }

            var lote = _loteService.GetLoteById(id);

            var actualizado = await _loteService.Update(id, cantidad, activo);

            return Ok(actualizado);
        }

        // POST: api/Lote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoteDto>> PostLote(LoteDto lote)
        {
            var loteCreado = await _loteService.Create(lote);

            return CreatedAtAction("GetLote", new { id = lote.Id }, lote);
        }

        // DELETE: api/Lote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLote(Guid id)
        {
            var lote = await _loteService.GetLoteById(id);
            if (lote == null)
            {
                return NotFound();
            }

            await _loteService.Delete(id);

            return Ok($"Se borró el producto con id {id} exitosamente");
        }
    }
}
