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
                IdRol = usuarioDto.IdRol,
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

        public async Task<GetUsuarioDto> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new ResourceNotFoundException($"User with Id {id} not found.");

            return new GetUsuarioDto()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                IsActive = usuario.Activo
            };
        }

        public async Task<List<GetUsuarioDto>> GetUsuarios()
        {
            var listaUsuarios = await _context.Usuarios.ToListAsync();

            if(listaUsuarios == null) throw new ResourceNotFoundException("No users in the Data Base");

            //Creada la lista la modifica de usuario a usuarioDto 
            var listaUsuariosDto = listaUsuarios.Select(u =>
                new GetUsuarioDto()
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

        //public async Task<Usuario> GetUsuarioByEmail(string email)
        //{

        //    try
        //    {
        //        //Intenta retornar el usuario del email correspondiente
        //        return await _context.Usuarios.FirstOrDefaultAsync(u => email == u.Email);

        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        // Captura detalles específicos de la base de datos
        //        throw new Exception("Error al guardar el usuario en la base de datos", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Captura otros errores
        //        throw new Exception("Ocurrió un error inesperado", ex);
        //    }

        //}

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

        public async Task<bool> UpdateUsuario(Guid id, PutUsuarioDto usuario)
        {
            //Busca el usuario en la base de datos
            var usuarioUpdate = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            //Si no lo encuentra se encarga de manejar el error
            if (usuarioUpdate == null)
            {
                // Lanzar una excepción personalizada si no se encuentra el usuario
                throw new ResourceNotFoundException($"The user with ID: {id} not found.");
            }
            //Si el usuario existe lo modifica y devuelve verdadero
            usuarioUpdate.Nombre = usuario.Nombre;
            usuarioUpdate.Apellido = usuario.Apellido;
            usuarioUpdate.Email = usuario.Email;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}

