using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        // Retorna um pedido pelo ID
        Task<PedidoEntity?> GetByIdAsync(int id);

        // Retorna todos os pedidos cadastrados.
        Task<IEnumerable<PedidoEntity>> GetAllAsync();

        // Adiciona um novo pedido no banco.
        Task AddAsync(PedidoEntity pedido);

        // Atualiza os dados de um pedido existente.
        Task UpdateAsync(PedidoEntity pedido);

        // Remove um pedido pelo ID.
        Task DeleteAsync(PedidoEntity pedido);
    }
}