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

            return new Usuario(dto.Nombre, dto.Apellido, dto.Password, dto.Email, dto.Tipo);
        
        }


    }
}
