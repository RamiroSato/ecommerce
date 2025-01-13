﻿using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Services
{

    public class UsuarioService : IUsuarioService
    {

        private readonly EcommerceContext _context;
                     

        public UsuarioService(EcommerceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }



        public async Task<Usuario> AddUsuario(Usuario usuario)
        {
            
            await _context.usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> GetUsuario(Guid id) 
        {

            return await _context.usuarios.FirstOrDefaultAsync(u => id == u.Id);
        
        }

        public async Task<List<Usuario>> GetUsuarios() 
        {
        
            return await _context.usuarios.ToListAsync();
        
        }

        public async Task<Usuario> GetUsuarioByEmail(string email) 
        {

            return await _context.usuarios.FirstOrDefaultAsync(u => email == u.Email);
        
        }

        public async Task<bool> DeleteUsuario(Guid id) 
        {

            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                return false;

            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var usuarioUpdate = await _context.usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);

            if (usuarioUpdate == null)
            {
                // Lanzar una excepción personalizada si no se encuentra el usuario
                throw new KeyNotFoundException($"Usuario con ID {usuario.Id} no encontrado.");
            }

            usuarioUpdate.Nombre = usuario.Nombre;
            usuarioUpdate.Apellido = usuario.Apellido;
            usuarioUpdate.Password = usuario.Password;
            usuarioUpdate.Email = usuario.Email;
            usuarioUpdate.Tipo = usuario.Tipo;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}

