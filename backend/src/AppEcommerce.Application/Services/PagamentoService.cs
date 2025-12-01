using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Enums;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _repo;
        private readonly IDictionary<FormaPagamentoEnum, PagamentoEntity> _estrategias;

        public PagamentoService(IPagamentoRepository repo)
        {
            _repo = repo;

            // Estratégias por forma de pagamento
            _estrategias = new Dictionary<FormaPagamentoEnum, PagamentoEntity>
            {
                { FormaPagamentoEnum.Pix, new PagamentoPix() },
                { FormaPagamentoEnum.CartaoCredito, new PagamentoCartaoCredito() }
                // Outras formas podem ser adicionadas aqui no futuro
            };
        }

        public async Task<IEnumerable<PagamentoEntity>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PagamentoEntity?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id do pagamento inválido.", nameof(id));

            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(PagamentoEntity pagamento)
        {
            if (pagamento is null)
                throw new ArgumentNullException(nameof(pagamento));

            await _repo.AddAsync(pagamento);
        }

        public async Task UpdateAsync(PagamentoEntity pagamento)
        {
            if (pagamento is null)
                throw new ArgumentNullException(nameof(pagamento));

            await _repo.UpdateAsync(pagamento);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id do pagamento inválido.", nameof(id));

            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException($"Pagamento com Id {id} não encontrado.");

            await _repo.DeleteAsync(existing);
        }

        public Task<decimal> CalcularValorComTaxaAsync(FormaPagamentoEnum forma, decimal valorPedido)
        {
            if (valorPedido < 0)
                throw new ArgumentException("Valor do pedido não pode ser negativo.", nameof(valorPedido));

            if (!_estrategias.TryGetValue(forma, out var estrategia))
            {
                // Se não tiver estratégia específica, considera taxa zero
                estrategia = new PagamentoPix();
            }

            var taxa = estrategia.CalcularTaxaServico(valorPedido);
            var total = valorPedido + taxa;

            return Task.FromResult(total);
        }
    }
}
