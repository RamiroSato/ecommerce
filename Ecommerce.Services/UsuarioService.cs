using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Services
{

    public class UsuarioService : IUsuarioService
    {

        private readonly EcommerceContext _context;


        public UsuarioService(EcommerceContext context)
        {
            _context = context;
        }


        public void AddUsuario(Usuario usuario) 
        {

            _context.usuarios.Add(usuario);
            _context.SaveChanges();
        
        }

        public async Task<Usuario> GetUsuario(Guid id) 
        {

            return await _context.Usuarios.FirstOrDefaultAsync(u => id == u.Id);
        
        }

        public async Task<List<Usuario>> GetUsuarios() 
        {
        
            return await _context.Usuarios.ToListAsync();
        
        }

        public async Task<Usuario> GetUsuarioByEmail(string email) 
        {

            return await _context.Usuarios.FirstOrDefaultAsync(u => email == u.Email);
        
        }

        public async Task<bool> DeleteUsuario(Guid id) 
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var existingUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            if (existingUsuario == null)
                return false;

            existingUsuario.Nombre = usuario.Nombre;
            existingUsuario.Apellido = usuario.Apellido;
            existingUsuario.Password = usuario.Password;
            existingUsuario.Email = usuario.Email;
            existingUsuario.Tipo = usuario.Tipo;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}

