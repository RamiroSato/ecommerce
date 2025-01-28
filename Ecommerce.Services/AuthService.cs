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

        private readonly IAmazonCognitoIdentityProvider _cognitoClient;
        private readonly string _clientId;
        private readonly string _userPoolId;
        private readonly IUsuarioService _usuarioService;

        public AuthService(IConfiguration config, IUsuarioService usuarioService)
        {
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
        }


        //Funciona, tengo que reveer como devolver el token de autenticacion
        public async Task<AuthResponse> RegisterAsync(UsuarioDto usuario)
        {
            try
            {
                var request = new AdminCreateUserRequest
                {
                    UserPoolId = _userPoolId,
                    Username = usuario.Email,
                    TemporaryPassword = usuario.Password,
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "email",  Value = usuario.Email },
                        new AttributeType { Name = "given_name", Value = usuario.Nombre}
                    }
                };

                var response = await _cognitoClient.AdminCreateUserAsync(request);

                usuario.CognitoId = response.User.Attributes
                                .First(a => a.Name == "sub").Value;

                if (usuario == null) { throw new ResourceNotFoundException($"Error"); }
                
                await _usuarioService.AddUsuario(usuario);

                return new AuthResponse
                {
                    IsSuccess = true,
                    Message = "Usuario registrado correctamente",
                    UserId = response.User.Attributes
                .First(a => a.Name == "sub").Value,

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
        //No funciona, no se si es por el metodo que no se esta utilizando de manera correcta
        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            try
            {
                var request = new InitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                    AuthParameters = new Dictionary<string, string>
                    {
                        { "USERNAME", email },
                        { "PASSWORD", password }
                    },
                    ClientId = _clientId
                };
                var response = await _cognitoClient.InitiateAuthAsync(request);

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
        //Ver si funciona bien con los ids de cognito 
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
    }

}

