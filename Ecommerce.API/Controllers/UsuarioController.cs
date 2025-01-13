using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Dtos.DtoMappers;


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
        [HttpPost("sing-in")]
        public async Task<IActionResult> Create([FromBody] UsuarioDto usuario)
        {
            try
            {
                var usuarioCreado = await _usuarioService.AddUsuario(usuario.UsuarioDtoAUsuario());
                return Ok(usuarioCreado);
            }
            catch
            {
                return BadRequest("No se pudo agregar el usuario");
            }
        }

        [HttpGet("get-all")]

        public async Task<IActionResult> GetAll() 
        {

            try
            {

                var listaUsuarios = await _usuarioService.GetUsuarios();
                return Ok(listaUsuarios);

            }
            catch (Exception ex) 
            {

                return BadRequest(new { Error = ex.Message });
            
            }
        
        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id)
        {
            try
            {
              
                var usuario = await _usuarioService.GetUsuario(id);

                // Verifica si el usuario es nulo
                if (usuario == null)
                {
                    return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });
                }

               
                return Ok(usuario);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { Error = "Ocurrió un error interno.", Details = ex.Message });
            }
        }

        //Siempre sale por el catch
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(Guid id) 
        {
            try
            {
                var usuario = new Usuario();

                if (_usuarioService.GetUsuario(id) != null)
                {
                    usuario = await _usuarioService.GetUsuario(id);
                    await _usuarioService.DeleteUsuario(id);
                }


                return Ok(usuario);

            }
            catch  
            {

                return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });
            
            }
        
        }
        //Siempre sale por el catch
        [HttpPut("id")]
        public async Task<IActionResult> ModificarUsuario(Guid id, UsuarioDto usuario) 
        {
            try
            {
              
                if (_usuarioService.GetUsuario(id) != null)
                {
                     await _usuarioService.UpdateUsuario(usuario.UsuarioDtoAUsuario());
                     
                }

               return Ok(usuario);

            }
            catch
            {

                return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });

            }

        }





    }
}
