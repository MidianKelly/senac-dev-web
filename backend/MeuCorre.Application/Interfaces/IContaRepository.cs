using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;

namespace MeuCorre.Application.Interfaces
{
    public class IContaRepository /// depois tem que herdar outro repository aqui
    {
        private readonly MeuDbContext _meuDbContext;
        public IContaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task<Categoria?> ObterContaPorIdAsync(Guid contaId)
        {
            var conta = await _meuDbContext.Contas.FindAsync(contaId);
            return conta;
        }

        public async Task<IEnumerable<Conta>> ListarTodasPorUsuarioAsync(Guid contaId)
        {
            var listaContas = _meuDbContext.Conta
                .Where(c => c.ContaId == contaId);

            return await listaContas.ToListAsync();
        }

        public async Task<bool> ExisteAsync(Guid contaId)
        {
            var existe = await _meuDbContext.Conta
                .AnyAsync(c => c.Id == contaId);

            return existe;
        }

        public async Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoConta tipoConta,TipoLimite tipoLimite, Guid contaId)
        {
            var existe = await _meuDbContext.Categorias
                .AnyAsync(
                            c => c.Nome == nome &&
                            c.TipoConta == tipoConta &&
                            c.TipoLimite == tipoLimite &&
                            c.contaId == contaId
                        );

            return existe;
        }

        public async Task AdicionarAsync(Conta conta)
        {
            _meuDbContext.Conta.Add(conta);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Conta conta)
        {
            _meuDbContext.Conta.Update(conta);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task RemoverAsync(Conta conta)
        {
            _meuDbContext.Conta.Remove(conta);
            await _meuDbContext.SaveChangesAsync();
        }
    }
}  