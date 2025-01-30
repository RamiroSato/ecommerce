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
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

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
                       
        
    }
}
