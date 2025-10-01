using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public ContaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId)
        {
            return await _meuDbContext.Contas
                  .Where(c => c.UsuarioId == usuarioId).SumAsync(c => c.Saldo);
        }

        public async Task<bool> ExisteContaComNomeAsync(Guid usuarioId, string nome, Guid? contaIdExcluir = null)
        {
            var existe = await _meuDbContext.Contas
                .AnyAsync(

                     c => c.Nome == nome && 
                     c.UsuarioId == usuarioId && 
                     (contaIdExcluir == null || c.Id != contaIdExcluir.Value)

                );
            return existe;
        }

        public async Task<Conta?> ObterPorIdAsync(Guid contaId)
        {
            var conta = await _meuDbContext.Contas.FindAsync(contaId);
            return conta;

        }

        public async Task<Conta?> ObterPorIdUsuarioAsync(Guid contaId, Guid usuarioId)
        {
            return await _meuDbContext.Contas
                .FirstOrDefaultAsync(c => c.Id == contaId && c.UsuarioId == usuarioId);
        }


        public async Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo)
        {
            return await _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId && c.TipoConta == tipo)
                .ToListAsync();
        }

        public async Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true)
        {
            var query = _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId);

            if (apenasAtivas)
            {
                query = query.Where(c => c.Ativo); // supondo que exista a propriedade Ativa
            }

            return await query
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
        public async Task AdicionarAsync(Conta conta)
        {
            await _meuDbContext.Contas.AddAsync(conta);
            await _meuDbContext.SaveChangesAsync();
        }

    }
}
