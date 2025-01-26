using Ecommerce.DTO;

namespace Ecommerce.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);

        Task<AuthResponse> LoginAsync(string email, string password);
    }
}