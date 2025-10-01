using MediatR;
using MeuCorre.Application.UseCases.Categorias.Dtos;
using MeuCorre.Application.UseCases.Categorias.Queries;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ListarContasQuery : IRequest<IList<Conta>>
    {
        public Guid UsuarioId { get; set; }                  // Id do usuário que pediu a lista
        public TipoConta? FiltrarPorTipo { get; set; }       // Filtra por tipo de conta (opcional)
        public bool ApenasAtivas { get; set; } = true;       // Se true, só mostra contas ativas
        public string OrdenarPor { get; set; } = "Nome";     // Campo para ordenar (ex: "Nome", "DataCriacao", etc)
    }


}
