﻿using Ecommerce.DTO;
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


        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] AuthDto usuario)
        {
            var result = await _authService.RegisterAsync(usuario, HttpContext.Items["cognitoId"]?.ToString());

            return Ok(result);
        }

        [HttpPost("confirm-singup")]
        public async Task<IActionResult> ConfirmSingUp(string email, string confimrAccountToken)
        {
            var result = await _authService.ConfirmAccount(email, confimrAccountToken);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authService.LoginAsync(email, password);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ActionResult(Guid id)
        {
            var result = await _authService.DeleteAsync(id);

            return Ok(result);
        }
    }

}
