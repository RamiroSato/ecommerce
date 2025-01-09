using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }



        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioService.AddUsuario(usuario);
                return Ok(usuario);
            }
            catch
            {
                return BadRequest("No se pudo agregar el usuario");
            }
        }

       
    }
}
