using Ecommerce.Models;
using Ecommerce.DTO;


namespace Ecommerce.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AddUsuario(UsuarioDto usuario);

        Task<GetUsuarioDto> GetUsuario(Guid id);

        Task<List<GetUsuarioDto>> GetUsuarios();

        //Task<Usuario> GetUsuarioByEmail(string email);

        Task<bool> DeleteUsuario(Guid id);

        Task<bool> UpdateUsuario(Guid id, PutUsuarioDto usuario);
    }
}
