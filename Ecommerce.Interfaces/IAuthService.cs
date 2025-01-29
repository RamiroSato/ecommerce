using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Ecommerce.DTO;

namespace Ecommerce.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(UsuarioDto usuario);

        Task<ConfirmSignUpResponse> ConfirmAccount(string email, string confirmAccountToken);

        Task<AuthResponse> LoginAsync(string email, string password);       

        
    }
}