using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class Categoria : Entidade
    {
        public string? Nome { get; private set; }

        public string? Descricao { get; private set; }
        public string? Cor { get; private set; }
        public string? Icone { get; private set; }
        public TipoTransacao Tipo { get; private set; }

        public Categoria (string nome, string descricao, string cor, string icone, TipoTransacao tipo)
        {            
            Nome = nome;
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Tipo = tipo;
        }

    }
}
    
