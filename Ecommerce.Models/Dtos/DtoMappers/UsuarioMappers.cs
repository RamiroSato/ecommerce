using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Tipo = dto.Tipo,
                IsActive = 1

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
