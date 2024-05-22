using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorLogin.Shared;

namespace BlazorLogin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            SesionDTO sesionDTO = new SesionDTO();

            //aqui se conecta a la BD. 

            if(login.Correo == "angel@gmail.com" && login.Clave == "admin")
            {
                sesionDTO.Nombre = "angel";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Administrador";
            }
            else
            {
                sesionDTO.Nombre = "empleado";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Empleado";
            }

            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }

    }
}
