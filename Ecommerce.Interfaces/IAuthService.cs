using Ecommerce.DTO;

namespace Ecommerce.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);

        Task<AuthResponse> LoginAsync(string email, string password);
    }
}