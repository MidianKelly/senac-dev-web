using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    class CriarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "ID do usuário é obrigatório!")]
        public required Guid UsuarioID { get; set; }
        [Required(ErrorMessage = "Nome da categoria é obrigatório!")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Tipo da transação (despesa ou receita) é obrigatório!")]
        public required  TipoTransacao Tipo { get; set; }

        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

    }

    internal class CriarCategoriaCommandHandler : IRequestHandler<CriarCategoriaCommand, (string, bool)>
    {

        private readonly ICategoriaRepository _categoriaRepository;
        public CriarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<(string, bool)> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
        {
            //NÃO PODE CADASTRAR CATEGORIA REPETIDA PARA O MESMO USUÁRIO
           var existe = await _categoriaRepository.NomeExisteParaUsuarioAsync(request.Nome, request.Tipo, request.UsuarioID);
            if (existe)
            {
                return ("Categoria já cadastrada", false);
            }
            //CÓDIGO PARA 
            var novaCategoria = new Categoria(
                request.UsuarioID,
                request.Nome,
                request.Tipo,
                request.Descricao,
                request.Cor,
                request.Icone
                );
            await _categoriaRepository.AdicionarAsync(novaCategoria);
            return ("Categoria cadastrada com sucesso", true);

        }
        
  
       
    }
    
}

