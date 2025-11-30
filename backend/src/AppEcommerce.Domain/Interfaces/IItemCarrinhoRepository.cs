using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces
{
    public interface IItemCarrinhoRepository
    {
        Task<ItemCarrinhoEntity?> GetByIdAsync(int id);
        Task<IEnumerable<ItemCarrinhoEntity>> GetAllAsync();
        Task AddAsync(ItemCarrinhoEntity entity);
        Task UpdateAsync(ItemCarrinhoEntity entity);
        Task DeleteAsync(int id);
    }
}
