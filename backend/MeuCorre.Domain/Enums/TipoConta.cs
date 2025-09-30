using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Enums
{
    public enum TipoConta
    {
        /// <summary>
        /// Pagamento realizado com dinheiro em espécie ou carteira digital (ex: PicPay, PayPal).
        /// </summary>
        
        Carteira = 1,

        /// <summary>
        /// Pagamento feito através de conta bancária, como transferência (TED, DOC, Pix) ou débito em conta.
        /// </summary>

        ContaBancaria = 2,

        /// <summary>
        /// Pagamento realizado com cartão de crédito, seja presencial ou online.
        /// </summary>
        
        CartaoCredito = 3
    }
}
