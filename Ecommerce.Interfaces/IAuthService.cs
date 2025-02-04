using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Ecommerce.DTO;

namespace Ecommerce.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(AuthDto usuario, string? requestedCognitoId);

        Task<ConfirmSignUpResponse> ConfirmAccount(string email, string confirmAccountToken);

        Task<AuthResponse> LoginAsync(string email, string password);

        Task<AuthResponse> DeleteAsync(Guid id);



    }
}