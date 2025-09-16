using MeuCorre.Domain.Enums;
using System;

namespace MeuCorre.Domain.Entities
{
    public class Categoria : Entidade
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public string? Descricao { get; private set; }
        public string? Cor { get; private set; }
        public string? Icone { get; private set; }
        public bool  Ativo { get; set; }

        //Propriedade de navegação para a entidade Usuario pois o usuário pode ter várias categorias
        public virtual Usuario Usuario { get; private set; }


        public Categoria(Guid usuarioId, string nome, TipoTransacao tipo, string? descricao,string? cor, string? icone)
        {
            UsuarioId = usuarioId;
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Tipo = tipo;
            Ativo = true;

        }
        public void AtualizarInformacoes (string nome, TipoTransacao tipo, bool ativo, string? descricao, string? cor, string? icone)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Tipo = tipo;
            Ativo = ativo;
            AtualizarDataMoficacao();
        }
        public void Ativar()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }
        public void Inativar()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }

    }
}
