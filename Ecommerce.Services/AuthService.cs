using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Ecommerce.Data.Contexts;
using Ecommerce.DTO;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.Extensions.Configuration;


namespace Ecommerce.Services
{
    public class AuthService(IConfiguration config, IUsuarioService usuarioService, IAmazonCognitoIdentityProvider cognitoClient) : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoClient = cognitoClient;
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly string _clientId = config.GetSection("AWS:AppClientId").Value;
        private readonly string _clientSecretId = config.GetSection("AWS:Client_secret_id").Value;

        public async Task<AuthResponse> RegisterAsync(UsuarioDto usuario)
        {
            var secretHash = SecretHasher.GenerateSecretHash(usuario.Email, _clientId, _clientSecretId);
            var hashedPassword = SecretHasher.GenerateSecretHash(usuario.Password, _clientSecretId);

            var request = new SignUpRequest
            {
                ClientId = _clientId,
                Username = usuario.Email,
                Password = hashedPassword,
                SecretHash = secretHash,
                UserAttributes = [
                        new AttributeType { Name = "email",  Value = usuario.Email },
                            new AttributeType { Name = "given_name", Value = usuario.Nombre}]
            };

            var response = await _cognitoClient.SignUpAsync(request);

            usuario.CognitoId = response.UserSub;
            usuario.Password = hashedPassword;

            if (usuario == null) { throw new ResourceNotFoundException($"Error"); }

            await _usuarioService.AddUsuario(usuario);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Usuario registrado correctamente",
                UserId = response.UserSub
            };
        }

        public async Task<ConfirmSignUpResponse> ConfirmAccount(string email, string confirmAccountToken)
        {
            var secretHash = SecretHasher.GenerateSecretHash(email, _clientId, _clientSecretId);

            var confirmResponse = await _cognitoClient.ConfirmSignUpAsync(new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = email,
                SecretHash = secretHash,
                ConfirmationCode = confirmAccountToken

            });

            return confirmResponse;

        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var secretHash = SecretHasher.GenerateSecretHash(email, _clientId, _clientSecretId);
            var hashedPassword = SecretHasher.GenerateSecretHash(password, _clientSecretId);

            var request = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                        {
                            { "USERNAME", email },
                            { "PASSWORD", hashedPassword },
                            { "SECRET_HASH", secretHash}
                        },
                ClientId = _clientId
            };
            var response = await _cognitoClient.InitiateAuthAsync(request);

            if (response.AuthenticationResult == null)
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Usuario o contraseña incorrectos"
                };

            Console.WriteLine(response.AuthenticationResult);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Usuario logueado correctamente",
                Tokens = response.AuthenticationResult
            };
        }

    }
}

