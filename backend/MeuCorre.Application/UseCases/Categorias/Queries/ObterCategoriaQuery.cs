using MediatR;
using MeuCorre.Application.UseCases.Categorias.Dtos;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Categorias.Queries
{
    public class ObterCategoriaQuery : IRequest<CategoriaDto>
    {
        public Guid CategoriaId { get; set; }
    }
    internal class ObterCategoriaQueryHandler : IRequest<ObterCategoriaQueryHandler>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public ObterCategoriaQueryHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task <CategoriaDto> Handle(ObterCategoriaQuery request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
            if (categoria == null)
                return null;

            var categoriaDto = new CategoriaDto
            {

                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                Tipo = categoria.Tipo,
                Cor = categoria.Cor,
                Descricao = categoria.Descricao,
                Icone = categoria.Icone,
            };
            return categoriaDto;
        }
    }
}
