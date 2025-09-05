using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        //Retono do banco de dados os dados de uma categoria que possui o id informado
        Task<Categoria>ObterPorIdAsync(Guid id);
        //Retorna do banco de dados todas as categorias que pertencem ao usuário informado
        Task<IEnumerable<Categoria>> ObterTodosAsync(Guid usuarioId);
        // Verificar se a categoria existe no banco de dados com o Id informado
        //SELECT * FROM Categoria WHERE Id = {id}
        Task<bool> ExisteAsync(Guid id);

        //Verifica se já existe uma categoria com o mesmo nome para o mesmo tipo de transação
        Task<bool> NomeExisteParacategoriaAsync (string nome, TipoTransacao Tipo, Guid usuarioId);
        //Adiciona uma nova categoria no banco de dados
        Task AdicionarAsync (Categoria categoria);

        //Atualizar uma nova categoria no banco de dados
        Task AtualizarAsync(Categoria categoria);

        //Remover categoria no banco de dados
        Task RemoverAsync(Categoria categoria);
    }
}
