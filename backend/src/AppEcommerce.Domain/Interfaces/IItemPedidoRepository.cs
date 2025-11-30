using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces
{
    public interface IItemPedidoRepository
    {
        // Retorna um item de pedido pelo ID
        Task<ItemPedidoEntity?> GetByIdAsync(int id);

        // Retorna todos os itens de pedidos cadastrados.
        Task<IEnumerable<ItemPedidoEntity>> GetAllAsync();

        // Adiciona um novo item no banco.
        Task AddAsync(ItemPedidoEntity item);

        // Atualiza os dados de um item existente.
        Task UpdateAsync(ItemPedidoEntity item);

        // Remove um item de pedido pelo ID.
        Task DeleteAsync(ItemPedidoEntity item);
    }
}