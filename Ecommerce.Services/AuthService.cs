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
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAmazonCognitoIdentityProvider _cognitoClient;
        private readonly string _clientId;
        private readonly string _userPoolId;
        private readonly IUsuarioService _usuarioService;
        private readonly SecretHasher _secretHasher;
        private readonly string _clientSecretId;

        public AuthService(IConfiguration config, IUsuarioService usuarioService, SecretHasher secretHasher)
        {
            _config = config;
            _cognitoClient = new AmazonCognitoIdentityProviderClient
                (
                new BasicAWSCredentials(
                config["AWS:Access_key_id"],
                config["AWS:Secret_access_key"]),
                RegionEndpoint.GetBySystemName(config["AWS:Region"])
                );

            _clientId = config.GetSection("AWS:AppClientId").Value;
            _userPoolId = config.GetSection("AWS:UserPoolId").Value;
            _usuarioService = usuarioService;
            _secretHasher = secretHasher;
            _clientSecretId = _config.GetSection("AWS:Client_secret_id").Value;
        }

        public async Task<AuthResponse> RegisterAsync(UsuarioDto usuario)
        {
            try
            {
                var secretHash = SecretHasher.GenerateSecretHash(usuario.Email, _clientId, _clientSecretId);

                var request = new SignUpRequest
                {
                    ClientId = _clientId,
                    Username = usuario.Email,
                    Password = usuario.Password,
                    SecretHash = secretHash,
                    UserAttributes = new List<AttributeType>
                        {
                            new AttributeType { Name = "email",  Value = usuario.Email },
                            new AttributeType { Name = "given_name", Value = usuario.Nombre}
                        }
                };

                var response = await _cognitoClient.SignUpAsync(request);

                usuario.CognitoId = response.UserSub;

                if (usuario == null) { throw new ResourceNotFoundException($"Error"); }

                await _usuarioService.AddUsuario(usuario);

                return new AuthResponse
                {
                    IsSuccess = true,
                    Message = "Usuario registrado correctamente",
                    UserId = response.UserSub

                };

            }

            catch (UsernameExistsException)
            {
                return new AuthResponse { IsSuccess = false, Message = "El usuario ya existe" };
            }

            catch (Exception ex)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
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
            try
            {
                var secretHash = SecretHasher.GenerateSecretHash(email, _clientId, _clientSecretId);

                var request = new InitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                    AuthParameters = new Dictionary<string, string>
                        {
                            { "USERNAME", email },
                            { "PASSWORD", password },
                            { "SECRET_HASH", secretHash}
                        },
                    ClientId = _clientId
                };
                var response = await _cognitoClient.InitiateAuthAsync(request);

                Console.WriteLine(response.AuthenticationResult);

                return new AuthResponse
                {
                    IsSuccess = true,
                    Message = "Usuario logueado correctamente",
                    Tokens = response.AuthenticationResult
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        

        
    }

}

