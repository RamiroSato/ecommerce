using Ecommerce.Models;


namespace Ecommerce.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AddUsuario(Usuario usuario);

        Task<Usuario> GetUsuario(Guid id);

        Task<List<Usuario>> GetUsuarios();

        Task<Usuario> GetUsuarioByEmail(string email);

        Task<bool> DeleteUsuario(Guid id);

        Task<bool> UpdateUsuario(Guid id, Usuario usuario);
    }
}
