using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Ecommerce.DTO;

namespace Ecommerce.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(UsuarioDto usuario);

        Task<AuthResponse> LoginAsync(string email, string password);

        Task<UserStatusType> GetAdminUserAsync(string userName);

        Task<List<UserType>> ListUsersAsync();
    }
}