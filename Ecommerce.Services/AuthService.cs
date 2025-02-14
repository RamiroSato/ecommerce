﻿using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Ecommerce.Data.Contexts;
using Ecommerce.DTO;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

//Podria manejar mejor el delet del usuario para que tambien lo elimine de cognito
//Tengo que agregar que cuando modifique el email del usuario en la base de datos tambien lo haga en cognito
//


namespace Ecommerce.Services
{
    public class AuthService(IConfiguration config, IUsuarioService usuarioService, IAmazonCognitoIdentityProvider cognitoClient, EcommerceContext context) : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoClient = cognitoClient;
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly string _clientId = config.GetSection("AWS:AppClientId").Value;
        private readonly string _clientSecretId = config.GetSection("AWS:ClientSecretId").Value;
        private readonly EcommerceContext _context = context;
        private readonly string _awsUserPoolId = config.GetSection("AWS:UserPoolId").Value;

        public async Task<AuthResponse> RegisterAsync(AuthDto usuario, string? requestedCognitoId)
        {
            int newUserRole = 2;
            //Si el usuario que se esta creando es admin, se verifica que el usuario que lo esta creando tenga el rol de admin
            if (!string.IsNullOrEmpty(requestedCognitoId))
            {
                var usuarioCognito = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.CognitoId == requestedCognitoId)
                    ?? throw new UnauthorizedAccessException("You are not authorized to create Admin users.");

                if (usuario.IdRol == 1 && usuarioCognito.IdRol != 1)
                    throw new UnauthorizedAccessException("Only admins can create other admin users.");

                if (usuario.IdRol == 1)
                    newUserRole = 1; // Set role como Admin

            }
            else if (usuario.IdRol == 1)
                throw new UnauthorizedAccessException("Only admins can create admin users.");


            var secretHash = SecretHasher.GenerateSecretHash(usuario.Email, _clientId, _clientSecretId);
            var hashedPassword = SecretHasher.GenerateSecretHash(usuario.Password, _clientSecretId);

            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Username = usuario.Email,
                Password = hashedPassword,
                SecretHash = secretHash,
                UserAttributes = [
                    new AttributeType { Name = "email", Value = usuario.Email },
                    new AttributeType { Name = "given_name", Value = usuario.Nombre }
                ]
            };

            //Agrega el usuario a cognito
            var signUpResponse = await _cognitoClient.SignUpAsync(signUpRequest);


            if (newUserRole == 1) // If Admin
            {
                var requestAdmin = new AdminAddUserToGroupRequest
                {
                    UserPoolId = _awsUserPoolId,
                    Username = usuario.Email,
                    GroupName = "Admin"
                };
                //Agrega el usuario al grupo admin en caso de que sea correcto
                var responseAdmin = await _cognitoClient.AdminAddUserToGroupAsync(requestAdmin);

                if (responseAdmin.HttpStatusCode != HttpStatusCode.OK)
                    throw new Exception("Failed to add admin user to group");

            }

            //Crea el usuario a agregar a la base de datos
            var usuarioDto = new UsuarioDto
            {
                IdRol = newUserRole,
                CognitoId = signUpResponse.UserSub,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Password = hashedPassword,
                Email = usuario.Email
            };

            //Agrega el usuario a la base de datos
            await _usuarioService.AddUsuario(usuarioDto);

            //Devuelve el resultado de la operacion
            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Usuario registrado correctamente",
                UserId = signUpResponse.UserSub
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
                Tokens = response.AuthenticationResult,
                UserId = _context.Usuarios.FirstOrDefault(u => u.Email == email)?.CognitoId ?? string.Empty
            };
        }

        public async Task<AuthResponse> DeleteAsync(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id) ?? throw new ResourceNotFoundException($"User with Id {id} not found.");

            //Eliminar el usuario de Cognito
            var deleteUserRequest = new AdminDeleteUserRequest
            {
                Username = usuario.Email,
                UserPoolId = _awsUserPoolId
            };
            await _cognitoClient.AdminDeleteUserAsync(deleteUserRequest);

            //Eliminar el usuario de la base de datos
            var usuarioDelete = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuarioDelete != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Usuario borrado correctamente",
                UserId = usuario.Id.ToString()
            };


        }

    }
}

