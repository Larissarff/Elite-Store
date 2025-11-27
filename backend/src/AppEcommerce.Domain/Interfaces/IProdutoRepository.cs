using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces;

public interface IProdutoRepository
{
    // 🔹 Retorna todos os produtos
    Task<IEnumerable<ProdutoEntity>> GetAllAsync();

    // 🔹 Retorna um produto específico pelo Id
    Task<ProdutoEntity?> GetByIdAsync(int id);

    // 🔹 Adiciona um novo produto ao banco
    Task AddAsync(ProdutoEntity produto);

    // 🔹 Atualiza um produto existente
    Task UpdateAsync(ProdutoEntity produto);

    // 🔹 Remove um produto
    Task DeleteAsync(ProdutoEntity produto);
}
