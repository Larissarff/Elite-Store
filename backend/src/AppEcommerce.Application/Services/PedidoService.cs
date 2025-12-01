using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repo;

        public PedidoService(IPedidoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PedidoEntity?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(PedidoEntity pedido)
        {
            await _repo.AddAsync(pedido);
        }

        public async Task UpdateAsync(PedidoEntity pedido)
        {
            await _repo.UpdateAsync(pedido);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is not null)
            {
                await _repo.DeleteAsync(existing);
            }
        }
    }
}
