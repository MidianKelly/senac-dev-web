using MediatR;
using System.Runtime.CompilerServices;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    public class CriarUsuariosCommands : IRequest<string>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

    }
}
