using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces
{
    public interface IPagamentoRepository
    {
        
        // Retorna um pagamento pelo ID
        Task<PagamentoEntity?> GetByIdAsync(int id);

        // Retorna todos os pagamentos cadastrados.
        Task<IEnumerable<PagamentoEntity>> GetAllAsync();

        // Adiciona um novo pagamento no banco.
        Task AddAsync(PagamentoEntity pagamento);

        /// Atualiza os dados de um pagamento existente.
        Task UpdateAsync(PagamentoEntity pagamento);

        // Remove um pagamento pelo ID.
        Task DeleteAsync(PagamentoEntity pagamento);

    }
}
