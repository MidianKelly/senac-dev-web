using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Entities
{
    public class Conta : Entidade
    {
        private TipoConta tipoConta;
        private TipoLimite tipoLimite;
        private decimal saldoAjustado;
        private int? diaVencimento;
        private int? diaFechamentoCalculado;

        public Guid ContaId { get; private set; }
        public string Nome { get; private set; }
        public TipoConta TipoConta { get; private set; }
        public TipoLimite TipoLimite { get; private set; }
        public decimal Saldo { get; private set; }
        public Guid UsuarioId { get; private set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; private set; }
        public Usuario Usuario { get; set; }


        public Conta(Guid contaId, Guid usuarioId, string nome, TipoConta tipoConta, TipoLimite tipoLimite, decimal saldo, bool ativo)
        {
            //aQUI TINHA UM MÉTODO DE VERIFICAR COR

            ContaId = contaId;
            UsuarioId = usuarioId;
            Nome = nome.ToUpper();
            TipoConta = tipoConta;
            TipoLimite = tipoLimite;
            Saldo = saldo;
            Ativo = true;

        }

        public Conta(Guid id, Guid usuarioId, string nome, TipoConta tipoConta, TipoLimite tipoLimite, decimal saldoAjustado, DateTime dataCriacao, int? diaVencimento, int? diaFechamentoCalculado) : base(id)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            this.tipoConta = tipoConta;
            this.tipoLimite = tipoLimite;
            this.saldoAjustado = saldoAjustado;
            DataCriacao = dataCriacao;
            this.diaVencimento = diaVencimento;
            this.diaFechamentoCalculado = diaFechamentoCalculado;
        }
    }

}
    
     




   
 




