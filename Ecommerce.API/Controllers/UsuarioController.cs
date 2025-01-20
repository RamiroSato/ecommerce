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
                //Captura errores al guardar los datos en la DB
                return BadRequest(new { Error = "Error al guardar los datos en la base de datos", Details = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {   
                //Captura otro tipo de errores
                return BadRequest(new { Error = "Ocurrió un error al procesar la solicitud", Details = ex.Message });
            }
        }



        //Metodo para mostrar toda la lista de usuarios de la Base de Datos
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() 
        {

            try
            {
                //Crea una lista de usuarios por medio de la funcion GetUsuarios del service
                var listaUsuarios = await _usuarioService.GetUsuarios();

                //Creada la lista la modifica de usuario a usuarioDto 
                var listaUsuariosDto = listaUsuarios.Select(u => u.usuarioADto()).ToList();

                //Retorna la lista de usuarioDtos
                return Ok(listaUsuariosDto);

            }
            //En caso de que ocurra un error lo captura y lo muestra
            catch (Exception ex) 
            {

                return BadRequest(new { Error = ex.Message });
            
            }
        
        
        }

        //Metodo para mostrar un usuario especifico
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id)
        {
            try
            {
              
                var usuario = await _usuarioService.GetUsuario(id);

                // Verifica si el usuario es nulo
                if (usuario == null)
                {
                    //Si el usuario no se encuentra muestra un error y el siguiente mensaje
                    return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });
                }

               //Si puede encontrar el usuario sin problemas lo retorna en formato dto
                return Ok(usuario.usuarioADto());
            }
            catch (Exception ex)
            {
               //Captura errores internos de la Base de Datos
                return StatusCode(500, new { Error = "Ocurrió un error interno.", Details = ex.Message });
            }
        }


        //Metodo para eliminar un suario especifico
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(Guid id) 
        {
            try
            {
                //Crea una variable para poder asignar el usuario en caso de que exista en la base de datos
                var usuario = new Usuario();
                usuario = await _usuarioService.GetUsuario(id);


                if(usuario == null)
                    //Si no encuentra el usuario a borrar muestra el notfound y el mensaje de error
                    return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });

                
                //Si el usuario se encuentra en la base de datos lo borra y retorna el usuario que se acaba de eliminar en formato dto
                await _usuarioService.DeleteUsuario(id);                
                return Ok(usuario.usuarioADto());

            }
            catch (Exception ex)
            {
                //Captura errores internos de la Base de Datos
                return StatusCode(500, new { Error = "Ocurrió un error interno.", Details = ex.Message });
            }

        }


        //Metodo para modificar usuarios
        [HttpPut("id")]
        public async Task<IActionResult> ModificarUsuario(Guid id, UsuarioDto usuario)
        {
            try
            {
                //Intenta hacer la modificacion al usuario
                var resultado = await _usuarioService.UpdateUsuario(id ,usuario.UsuarioDtoAUsuario());
                //Si la modificacion fue exitosa devuelve verdadero 
               
                if (!resultado)
                {
                    //Si no se pudo realizar la modificacion retorna el error
                    return NotFound(new { Message = $"El id '{id}' no corresponde a ningún usuario en la base de datos." });
                }

                //Retorno del ok y el usuario modificado
                return Ok("Usuario modificado con exito");
            }

            //Captura errores y los retorna
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Details = ex.Message });
            }
        }






    }
}
