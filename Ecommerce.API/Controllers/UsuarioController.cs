using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Dtos.DtoMappers;
using Microsoft.EntityFrameworkCore;


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
                //Intenta crear el usuario
                var usuarioCreado = await _usuarioService.AddUsuario(usuario.UsuarioDtoAUsuario());
                //Si todo sale bien, devuelve un 200 con el usuario creado recientemente
                return Ok(usuarioCreado.usuarioADto());
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { Error = "Error al guardar los datos en la base de datos", Details = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Ocurrió un error al procesar la solicitud", Details = ex.Message });
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
                usuario = await _usuarioService.GetUsuario(id);
                await _usuarioService.DeleteUsuario(id);                
                return Ok(usuario);

            }
            catch  
            {

                return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });
            
            }
        
        }
        [HttpPut("id")]
        public async Task<IActionResult> ModificarUsuario(Guid id, UsuarioDto usuario)
        {
            try
            {
                var resultado = await _usuarioService.UpdateUsuario(id ,usuario.UsuarioDtoAUsuario());
                if (!resultado)
                {
                    return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });
                }
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Details = ex.Message });
            }
        }






    }
}
