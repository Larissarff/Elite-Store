using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoEntity>> GetAllAsync();
        Task<PedidoEntity?> GetByIdAsync(int id);
        Task AddAsync(PedidoEntity pedido);
        Task UpdateAsync(PedidoEntity pedido);
        Task DeleteAsync(int id);
    }
}
