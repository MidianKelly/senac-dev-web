using MeuCorre.Application.UseCases.Usuarios.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        ///<cria um novo usuário>
        ///Cria um novo usuário
        ///<param name="command"><param>
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuariosCommands command)
        {
            

        }
    }
       
}
    
