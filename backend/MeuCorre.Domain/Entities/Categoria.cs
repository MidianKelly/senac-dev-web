using MeuCorre.Domain.Enums;
using System;
using System.Text.RegularExpressions;

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
            ValidarEntidadeCategoria(cor);
            UsuarioId = usuarioId;
            Nome = nome.ToUpper();
            Tipo = tipo;
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Ativo = true;

        }
        public void AtualizarInformacoes (string nome, TipoTransacao tipo, string? descricao, string? cor, string? icone)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Tipo = tipo;
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
        private void ValidarEntidadeCategoria (string cor)
        {
            if (string.IsNullOrEmpty(cor))
            {
                return; //retornar caso a cor seja nula ou vazia 
            }

            var corRegex = new Regex(@"^#?([0-9a-fA-F]{3}){1,2}$"); //Regex serve para criar verificação de cores através de comandos

            if (!corRegex.IsMatch(cor))
            {
                throw new Exception("Cor inválida. Deve ser um código hexadecimal.");
            }
        }

    }
}
