using System.ComponentModel.DataAnnotations;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MediatR;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class CriarContaCommand : IRequest<CriarContaResponse>
    {
        public Guid ContaId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "É necessário informar o Nome da conta!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre 2 e 50 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "É necessário informar o Tipo da conta!")]
        public TipoConta TipoConta { get; set; }

        [Required(ErrorMessage = "É necessário informar o Tipo do limite!")]
        public TipoLimite TipoLimite { get; set; }

        public decimal? Saldo { get; set; }

        [Required(ErrorMessage = "É necessário informar o Id do usuário!")]
        public Guid UsuarioId { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Propriedades específicas para Cartão de Crédito
        public int? DiaVencimento { get; set; }  // (1-31)
        public int? DiaFechamento { get; set; }  // (calculado se não informado)
    }

    public class CriarContaResponse
    {
        public Conta? Conta { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Sucesso { get; set; }
    }

    internal class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, CriarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public CriarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<CriarContaResponse> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            // Verifica se já existe conta com o mesmo nome para o usuário
            bool nomeExiste = await _contaRepository.ExisteContaComNomeAsync(request.UsuarioId, request.Nome);

            if (nomeExiste)
            {
                return new CriarContaResponse
                {
                    Mensagem = "Já existe uma conta com esse nome para o usuário.",
                    Sucesso = false
                };
            }

            // Ajustar saldo negativo se for devedor (saldo < 0)
            decimal saldoAjustado = request.Saldo ?? 0m;
            if (saldoAjustado < 0)
                saldoAjustado = saldoAjustado * -1;

            // Se tipo for Cartão de Crédito, validar DiaVencimento e calcular DiaFechamento se necessário
            int? diaFechamentoCalculado = request.DiaFechamento;
            if (request.TipoConta == TipoConta.CartaoCredito)
            {
                if (!request.DiaVencimento.HasValue || request.DiaVencimento < 1 || request.DiaVencimento > 31)
                {
                    return new CriarContaResponse
                    {
                        Mensagem = "Dia de vencimento obrigatório e deve ser entre 1 e 31 para cartão de crédito.",
                        Sucesso = false
                    };
                }

                if (!diaFechamentoCalculado.HasValue)
                {
                    int diaFechamento = request.DiaVencimento.Value - 10;
                    if (diaFechamento < 1)
                        diaFechamento += 31; // Ajusta se for negativo, assume mês anterior

                    diaFechamentoCalculado = diaFechamento;
                }
            }

            // Criar entidade Conta
            var novaConta = new Conta(
                request.ContaId,
                request.UsuarioId,
                request.Nome,
                request.TipoConta,
                request.TipoLimite,
                saldoAjustado,
                request.DataCriacao,
                request.DiaVencimento,
                diaFechamentoCalculado
            );

            // Salvar no repositório
            await _contaRepository.AdicionarAsync(novaConta);

            return new CriarContaResponse
            {
                Conta = novaConta,
                Mensagem = "Conta criada com sucesso.",
                Sucesso = true
            };
        }
    }
}
