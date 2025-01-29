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
        }


        //Funciona, ver si es el metodo correcto para la creacion de usuarios, si no deberia ser modificado
        public async Task<AuthResponse> RegisterAsync(UsuarioDto usuario)
        {
            try
            {
                var client_secret_id = _config.GetSection("AWS:Client_secret_id").Value;
                var secretHash = SecretHasher.GenerateSecretHash(usuario.Email, _clientId, client_secret_id);

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
            var client_secret_id = _config.GetSection("AWS:Client_secret_id").Value;
            var secretHash = SecretHasher.GenerateSecretHash(email, _clientId, client_secret_id);

            var confirmResponse = await _cognitoClient.ConfirmSignUpAsync(new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = email,
                SecretHash = secretHash,
                ConfirmationCode = confirmAccountToken

            });

            return confirmResponse;

        }

        //No funciona, no se si es por el metodo que no se esta utilizando de manera correcta
        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            try
            {
                var client_secret_id = _config.GetSection("AWS:Client_secret_id").Value;
                var secretHash = SecretHasher.GenerateSecretHash(email, _clientId, client_secret_id);

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


        //Cambiar el metodo para que no sea solo del admin
        public async Task<UserStatusType> GetAdminUserAsync(string userName)
        {
            AdminGetUserRequest userRequest = new AdminGetUserRequest
            {
                Username = userName,
                UserPoolId = _userPoolId,
            };

            var response = await _cognitoClient.AdminGetUserAsync(userRequest);

            Console.WriteLine($"User status {response.UserStatus}");
            return response.UserStatus;
        }



        //Funciona, pero devuelve los datos medios desorganizados
        public async Task<List<UserType>> ListUsersAsync()
        {
            var userPoolId = _userPoolId;

            var request = new ListUsersRequest
            {
                UserPoolId = userPoolId
            };

            var users = new List<UserType>();

            var usersPaginator = _cognitoClient.Paginators.ListUsers(request);
            await foreach (var response in usersPaginator.Responses)
            {
                users.AddRange(response.Users);
            }

            return users;
        }



        //Necesito hacer funcionar el metodo para cambiar la password, por el momento solo devuelve falso 
        public async Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            try
            {
                // Autenticación para obtener AccessToken
                var authRequest = new InitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                    ClientId = _clientId,
                    AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", email },
                { "PASSWORD", oldPassword },
                { "SECRET_HASH", SecretHasher.GenerateSecretHash(email, _clientId, _config.GetSection("AWS:Client_secret_id").Value) }
            }
                };

                var authResponse = await _cognitoClient.InitiateAuthAsync(authRequest);

                if (authResponse.AuthenticationResult == null)
                {
                    Console.WriteLine("Error: No se obtuvo un AccessToken.");
                    return false;
                }

                // Acceso con el token obtenido
                var accessToken = authResponse.AuthenticationResult.AccessToken;

                var changePasswordRequest = new ChangePasswordRequest
                {
                    PreviousPassword = oldPassword,
                    ProposedPassword = newPassword,
                    AccessToken = accessToken // Aquí pasamos el token válido
                };

                await _cognitoClient.ChangePasswordAsync(changePasswordRequest);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar la contraseña: {ex.Message}");
                return false;
            }
        }

    }

}

