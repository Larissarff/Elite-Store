using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoEntity>> GetAllAsync();
        Task<ProdutoEntity?> GetByIdAsync(int id);
        Task AddAsync(ProdutoEntity produto);
        Task UpdateAsync(ProdutoEntity produto);
        Task DeleteAsync(int id);
    }
}
