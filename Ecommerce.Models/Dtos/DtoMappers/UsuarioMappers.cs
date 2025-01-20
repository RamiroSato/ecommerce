

using System.Net.Http.Headers;

namespace Ecommerce.Models.Dtos.DtoMappers
{
    public static class UsuarioMappers
    {
        public static Usuario UsuarioDtoAUsuario(this UsuarioDto dto) 
        {

            return new Usuario
            {

                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Password = dto.Password,
                Email = dto.Email,
                IdRol = dto.IdRol,
                
            };
        
        }

        public static GetUsuarioDto usuarioADto(this Usuario usuario) 
        {

            return new GetUsuarioDto
            {

                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                IsActive = usuario.IsActive


            };
        
        }


    }
}
