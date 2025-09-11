using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    public class CriarUsuariosCommands : IRequest<(string, bool)>
    {
        [Required (ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe uma senha")]
        [MaxLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe sua data de nascimentp")]
        public DateTime DataNascimento { get; set; }

    }
    internal class CriarUsuariosCommandHandler : IRequestHandler<CriarUsuariosCommands, (string,bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public CriarUsuariosCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Handler significa "manipulador". Tudo dentro do handler define o que será feito no sistema
        // em resposta a uma determinada ação ou evento.
        public async Task<(string,bool)> Handle(CriarUsuariosCommands request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepository.ObterUsuarioPorEmail(request.Email);
            if(usuarioExistente != null)
            {
                return ("Já existe um usuário cadastrado com esse email", false);
            }
            var ano = DateTime.Now.Year;
            var idade = ano - request.DataNascimento.Year;
            if (idade < 13)
            {
                return ("Usuário deve ter no mínimo 13 anos",false);
            }
            var novoUsuario = new Usuario(
                request.Nome,
                request.Email,               
                request.DataNascimento,
                request.Senha,
                true);
            await _usuarioRepository.CriarUsuarioAsync(novoUsuario);
            return ("Usuário criado com sucesso", true);

        }
    }
}
