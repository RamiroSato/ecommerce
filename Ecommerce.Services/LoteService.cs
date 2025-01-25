using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class LoteService : ILoteService
    {
        private readonly EcommerceContext _context;

        public LoteService(EcommerceContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Lote>> GetLotesPorProducto(Guid id)
        {
            return await _context.Lotes
                .Where(l => l.IdProducto == id)
                .Select(l => new Lote
                {
                    Id = l.Id,
                    IdProducto = l.IdProducto,
                    Descripcion = l.Descripcion,
                    Cantidad = l.Cantidad,
                    Activo = l.Activo,
                    FechaAlta = l.FechaAlta,
                }).ToListAsync();
        }

        public async Task<Lote> GetLoteById(Guid id)
        {
            return await _context.Lotes.FindAsync(id);
        }

        public Lote LoteDtoALote(LoteDto loteDto)
        {
            return new Lote
            {
                Id = loteDto.Id,
                IdProducto = loteDto.IdProducto,
                Descripcion = loteDto.Descripcion,
                Cantidad = loteDto.Cantidad,
                Activo = loteDto.Activo,
                FechaAlta = loteDto.FechaAlta,
            };
        }

        public async Task<LoteDto> Create(LoteDto nuevoLote)
        {
            if (nuevoLote.Id == Guid.Empty)
            {
                nuevoLote.Id = Guid.NewGuid();
            }

            if(!_context.Lotes.Any(l => l.Id == nuevoLote.Id))
            {
                _context.Lotes.Add(LoteDtoALote(nuevoLote));
                await _context.SaveChangesAsync();
                return nuevoLote = new LoteDto
                {
                    IdProducto = nuevoLote.IdProducto,
                    Descripcion = nuevoLote.Descripcion
                };
            } else
            {
                return null;
            }
        }

        public async Task<Lote> Update(Guid id, int cantidad, bool activo)
        {
            var lote = await _context.Lotes.FindAsync(id);

            if (lote == null)
            {
                return null;
                throw new ArgumentException("No se encontró el lote declarado");
            }

            lote.Cantidad = cantidad;
            lote.Activo = activo;

            _context.Lotes.Update(lote);
            await _context.SaveChangesAsync();

            return lote;
        }

        public async Task<bool> Delete(Guid id)
        {
            var lote = await _context.Lotes.FindAsync(id);

            _context.Lotes.Remove(lote);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
