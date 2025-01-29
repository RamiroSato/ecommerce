using Ecommerce.DTO;
using Ecommerce.Interfaces;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            
        }



        //O no va, o se tiene que modificar para que coincida con el modelo de datos
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] UsuarioDto usuario)
        {
            var result = await _authService.RegisterAsync(usuario);
            
            return Ok(result);
        }

        [HttpPost("confirm-singup")]
        public async Task<IActionResult> ConfirmSingUp(string email, string confimrAccountToken)
        {
            var result = await _authService.ConfirmAccount(email, confimrAccountToken);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(result);
        }



        [HttpGet("id")]
        public async Task<IActionResult> Get(string userName)
        {

            var user = await _authService.GetAdminUserAsync(userName);

            return Ok(user);

        }

        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAll() 
        {

            var users = await _authService.ListUsersAsync();

            return Ok(users);

        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(string email, string oldPassword, string newPassword)
        {

            var result = await _authService.ChangePasswordAsync(email, oldPassword, newPassword);

            return Ok(result);


        }

    }
}
