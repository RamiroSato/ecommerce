using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.DTO;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Services
{

    public class UsuarioService(EcommerceContext context) : IUsuarioService
    {
        private readonly EcommerceContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Usuario> AddUsuario(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
                throw new ArgumentNullException(nameof(usuarioDto));

            try
            {
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

                Rol? rolExistente = await _context.Roles.FindAsync(usuarioDto.IdRol) ?? throw new Exception($"El rol con Id {usuarioDto.IdRol} no existe.");

                await _context.Usuarios.AddAsync(usuarioToAdd);
                await _context.SaveChangesAsync();
                return usuarioToAdd;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar el usuario en la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado", ex);
            }
        }

        public async Task<Usuario> GetUsuario(Guid id)
        {

            try
            {

                return await _context.Usuarios.FirstOrDefaultAsync(u => id == u.Id);

            }
            catch (DbUpdateException ex)
            {
                // Captura detalles específicos de la base de datos
                throw new Exception("Error al guardar el usuario en la base de datos", ex);
            }
            catch (Exception ex)
            {
                // Captura otros errores
                throw new Exception("Ocurrió un error inesperado", ex);
            }

        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            try
            {
                //intenta retornar la lista de usuarios
                return await _context.Usuarios.ToListAsync();

            }
            catch (DbUpdateException ex)
            {
                // Captura detalles específicos de la base de datos
                throw new Exception("Error al guardar el usuario en la base de datos", ex);
            }
            catch (Exception ex)
            {
                // Captura otros errores
                throw new Exception("Ocurrió un error inesperado", ex);
            }

        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {

            try
            {
                //Intenta retornar el usuario del email correspondiente
                return await _context.Usuarios.FirstOrDefaultAsync(u => email == u.Email);

            }
            catch (DbUpdateException ex)
            {
                // Captura detalles específicos de la base de datos
                throw new Exception("Error al guardar el usuario en la base de datos", ex);
            }
            catch (Exception ex)
            {
                // Captura otros errores
                throw new Exception("Ocurrió un error inesperado", ex);
            }

        }

        public async Task<bool> DeleteUsuario(Guid id)
        {
            //Busca el usuario en la base de datos
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            //Si no lo encuentra devuelve false 
            if (usuario == null)
                return false;

            //Si el usuario existe en la base de datos lo elimina
            _context.Usuarios.Remove(usuario);

            //Se guardan los cambios en la base de datos y devuelve true
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateUsuario(Guid id, Usuario usuario)
        {
            //Busca el usuario en la base de datos
            var usuarioUpdate = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            //Si no lo encuentra se encarga de manejar el error
            if (usuarioUpdate == null)
            {
                // Lanzar una excepción personalizada si no se encuentra el usuario
                throw new KeyNotFoundException($"Usuario con ID {usuario.Id} no encontrado.");
            }

            //Si el usuario existe lo modifica y devuelve verdadero
            usuarioUpdate.Nombre = usuario.Nombre;
            usuarioUpdate.Apellido = usuario.Apellido;
            usuarioUpdate.Email = usuario.Email;

            //usuarioUpdate.Tipo = usuario.Tipo;//Se tiene que poder cambiar de rol?
            //usuarioUpdate.Password = usuario.Password;//Cambiar de lugar la forma de modificar la contraseña 
            //Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

