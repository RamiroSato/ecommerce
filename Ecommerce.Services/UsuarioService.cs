using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.DTO;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Exceptions;


namespace Ecommerce.Services
{
    public class UsuarioService(EcommerceContext context) : IUsuarioService
    {
        private readonly EcommerceContext _context = context;

        public async Task<Usuario> AddUsuario(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
                throw new ResourceNotFoundException(nameof(usuarioDto));

            var usuarioToAdd = new Usuario()
            {
                IdRol = usuarioDto.IdRol, // TODO: Validar quien puede dar de alta usuarios con roles diferentes
                CognitoId = usuarioDto.CognitoId,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                Password = usuarioDto.Password,
                Email = usuarioDto.Email,
                Activo = true
            };

            // Busca el rol existente en la base de datos

            Rol? rolExistente = await _context.Roles.FindAsync(usuarioDto.IdRol) ?? throw new ResourceNotFoundException($"Rol with Id {usuarioDto.IdRol} not found.");

            await _context.Usuarios.AddAsync(usuarioToAdd);

            await _context.SaveChangesAsync();

            return usuarioToAdd;
        }

        public async Task<UsuarioGetDto> GetUsuario(Guid id, string? CognitoId)
        {

            var usuario = await _context.Usuarios.FindAsync(id) ?? throw new ResourceNotFoundException($"User with Id {id} not found.");
            var usuarioCognito = await _context.Usuarios.FirstOrDefaultAsync(u => u.CognitoId == CognitoId) ?? throw new UnauthorizedAccessException("You are not authorized to see this user.");

            //if user is not admin and is trying to see another user that is not themself
            if (usuario.CognitoId != CognitoId && usuarioCognito.IdRol != 1)
                throw new UnauthorizedAccessException("You are not authorized to see this user.");

            return new UsuarioGetDto()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                IsActive = usuario.Activo
            };
        }

        public async Task<List<UsuarioGetDto>> GetUsuarios()
        {
            var listaUsuarios = await _context.Usuarios.ToListAsync();

            if(listaUsuarios == null) throw new ResourceNotFoundException("No users in the Data Base");

            //Creada la lista la modifica de usuario a usuarioDto 
            var listaUsuariosDto = listaUsuarios.Select(u =>
                new UsuarioGetDto()
                {
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email,
                    IsActive = u.Activo
                }
            ).ToList();
            //intenta retornar la lista de usuarios
            return listaUsuariosDto;
        }

        public async Task<bool> DeleteUsuario(Guid id)
        {
            //Busca el usuario en la base de datos
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ResourceNotFoundException("User not found");

            //Si el usuario existe en la base de datos lo elimina
            _context.Usuarios.Remove(usuario);

            //Se guardan los cambios en la base de datos y devuelve true
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateUsuario(Guid id, PutUsuarioDto usuario, string? requestedCognitoId)
        {
            //Busca el usuario en la base de datos
            //var query = _context.Usuarios.AsQueryable();
            //query = query.Where(u => u.Id == id);


            var usuarioUpdate = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ResourceNotFoundException($"The user with ID: {id} not found.");
            var usuarioCognito = await _context.Usuarios.FirstOrDefaultAsync(u => u.CognitoId == requestedCognitoId) ?? throw new UnauthorizedAccessException("You are not authorized to modify this user.");

            //if user is not admin and is trying to modify another user that is not themself
            if (usuarioUpdate.CognitoId != requestedCognitoId && usuarioCognito.IdRol != 1)
                throw new UnauthorizedAccessException("You are not authorized to modify this user.");

            //Si el usuario existe lo modifica y devuelve verdadero
            usuarioUpdate.Nombre = usuario.Nombre;
            usuarioUpdate.Apellido = usuario.Apellido;
            usuarioUpdate.Email = usuario.Email;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

