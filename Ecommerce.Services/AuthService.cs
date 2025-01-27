using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Ecommerce.DTO;
using Ecommerce.Interfaces;
using Microsoft.Extensions.Configuration;


namespace Ecommerce.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAmazonCognitoIdentityProvider _cognitoClient;
        private readonly string _clientId;
        private readonly string _userPoolId;
        
        public AuthService(IConfiguration config)
        {
            _cognitoClient = new AmazonCognitoIdentityProviderClient(RegionEndpoint.GetBySystemName(config.GetSection("AWS:Region").Value));
            _clientId = config.GetSection("AWS:AppClientId").Value;
            _userPoolId = config.GetSection("AWS:UserPoolId").Value;            
        }

        public async Task<AuthResponse> RegisterAsync(string email, string password)
        {
            try
            {
                var request = new SignUpRequest
                {
                    ClientId = _clientId,
                    Username = email,
                    Password = password,
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType
                        {
                            Name = "email",
                            Value = email
                        }
                    }
                };
                var response = await _cognitoClient.SignUpAsync(request);

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
        }

    }

