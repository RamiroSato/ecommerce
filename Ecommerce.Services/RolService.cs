using Ecommerce.Data.Contexts;
using Ecommerce.DTO;
using Ecommerce.Exceptions;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class RolService(EcommerceContext context) : IRolService
    {
        private readonly EcommerceContext _context = context;
        public async Task<Rol> AddRolAsync(Rol rol)
        {
            _context.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task DeleteRolAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id)
                ?? throw new ResourceNotFoundException("Rol to delete not found");
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Rol>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> UpdateRolAsync(RolToUpdateDTO rolToUpdate)
        {
            var rol = await _context.Roles.FindAsync(rolToUpdate.Id) ?? throw new ResourceNotFoundException("Rol to update not found");
            rol.Descripcion = rolToUpdate.Descripcion;
            rol.Activo = rolToUpdate.Activo;

            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol> GetRolByCognitoIdAsync(string cognitoId)
        {   
            var roles = _context.Roles;
            var usuarios = _context.Usuarios;
            var query = roles.AsQueryable()
                .Join(usuarios, r => r.Id, u => u.IdRol, (r, u) => new { r, u })
                .Where(x => x.u.CognitoId == cognitoId);

            var rol = await query.Select(x => x.r).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Rol not found");       

            return rol;
        }
    }
}
