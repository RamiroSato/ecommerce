using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Humanizer;


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
            await _usuarioService.AddUsuario(usuario);
            return Ok("Usuario creado con exito");
        }


        //Metodo para mostrar toda la lista de usuarios de la Base de Datos
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            //Crea una lista de usuarios por medio de la funcion GetUsuarios del service
            var listaUsuarios = await _usuarioService.GetUsuarios();
            //Retorna la lista de usuarioDtos
            return Ok(listaUsuarios);
        }

        //Metodo para mostrar un usuario especifico
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioService.GetUsuario(id);
            //Si puede encontrar el usuario sin problemas lo retorna en formato dto
            return Ok(usuario);
        }


        //Metodo para eliminar un suario especifico
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            //Si el usuario se encuentra en la base de datos lo borra y retorna el usuario que se acaba de eliminar en formato dto
            await _usuarioService.DeleteUsuario(id);
            return Ok();
        }

        //Metodo para modificar usuarios
        [HttpPut("id")]
        public async Task<IActionResult> ModificarUsuario(Guid id, UsuarioDto usuario)
        {

            //Intenta hacer la modificacion al usuario
            var resultado = await _usuarioService.UpdateUsuario(id, new PutUsuarioDto()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
            });

            //Retorno del ok y el usuario modificado
            return Ok("Usuario modificado con exito");

        }






    }
}
