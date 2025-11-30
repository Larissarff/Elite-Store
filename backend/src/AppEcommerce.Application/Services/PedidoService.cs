using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _repo;

        public PedidoService(IPedidoRepository repo)
        {
            _repo = repo;
        }

        // 🔹 Buscar todos os pedidos
        public async Task<IEnumerable<PedidoEntity>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        // 🔹 Buscar um pedido pelo ID
        public async Task<PedidoEntity?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        // 🔹 Cadastrar novo pedido
        public async Task AddAsync(PedidoEntity pedido)
        {
            await _repo.AddAsync(pedido);
        }

        // 🔹 Atualizar pedido
        public async Task UpdateAsync(PedidoEntity pedido)
        {
            await _repo.UpdateAsync(pedido);
        }

        // 🔹 Excluir pedido
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