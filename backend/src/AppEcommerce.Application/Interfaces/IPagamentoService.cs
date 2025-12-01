using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Enums;

namespace AppEcommerce.Application.Interfaces
{
    public interface IPagamentoService
    {
        Task<IEnumerable<PagamentoEntity>> GetAllAsync();
        Task<PagamentoEntity?> GetByIdAsync(int id);
        Task AddAsync(PagamentoEntity pagamento);
        Task UpdateAsync(PagamentoEntity pagamento);
        Task DeleteAsync(int id);

        /// <summary>
        /// Exemplo de uso de polimorfismo para regra variável:
        /// calcula o valor final do pagamento (pedido + taxa)
        /// de acordo com a forma de pagamento.
        /// </summary>
        Task<decimal> CalcularValorComTaxaAsync(FormaPagamentoEnum forma, decimal valorPedido);
    }
}
