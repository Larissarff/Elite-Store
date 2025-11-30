using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces
{
    public interface ICarrinhoRepository
    {

        // Retorna um carrinho pelo ID
        Task<CarrinhoEntity?> GetByIdAsync(int id);

        // Retorna todos os carinhos(?) cadastrados.
        Task<IEnumerable<CarrinhoEntity>> GetAllAsync();

        // Adiciona um novo carrinho no banco.
        Task AddAsync(CarrinhoEntity carrinho);

        /// Atualiza os dados de um carrinho existente.
        Task UpdateAsync(CarrinhoEntity carrinho);

        /// Remove um carrinho pelo ID.
        Task DeleteAsync(int id);
    }
}