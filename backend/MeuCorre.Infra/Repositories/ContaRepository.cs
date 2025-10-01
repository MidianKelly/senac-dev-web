using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Repositories
{
    class ContaRepository : IContaRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public ContaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExisteContaComNomeAsync(Guid usuarioId, string nome, Guid? contaIdExcluir = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Conta?> ObterPorIdAsync(Guid contaId)
        {
            var conta = await _meuDbContext.Contas.FindAsync(contaId);
            return conta;

        }

        public Task<Conta?> ObterPorIdEUsuarioAsync(Guid contaId, Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true)
        {
            throw new NotImplementedException();
        }
    }
}
