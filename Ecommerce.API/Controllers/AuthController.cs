using Ecommerce.DTO;
using Ecommerce.Interfaces;
using Ecommerce.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _authService.RegisterAsync(usuario.Email, usuario.Password);
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
