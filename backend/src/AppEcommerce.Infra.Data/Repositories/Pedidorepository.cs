using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly JsonContext _context;
        private const string FileName = "pedidos.json";

        public PedidoRepository(JsonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllAsync()
            => await _context.GetAsync<PedidoEntity>(FileName);

        public async Task<PedidoEntity?> GetByIdAsync(int id)
        {
            var lista = await _context.GetAsync<PedidoEntity>(FileName);
            return lista.FirstOrDefault(p => p.IdPedido == id);
        }

        public async Task AddAsync(PedidoEntity pedido)
        {
            var lista = await _context.GetAsync<PedidoEntity>(FileName);

            int novoId = lista.Any() ? lista.Max(p => p.IdPedido) + 1 : 1;
            pedido.DefinirId(novoId);

            lista.Add(pedido);
            await _context.SaveAsync(FileName, lista);
        }

        public async Task UpdateAsync(PedidoEntity pedido)
        {
            var lista = await _context.GetAsync<PedidoEntity>(FileName);
            var index = lista.FindIndex(p => p.IdPedido == pedido.IdPedido);

            if (index != -1)
            {
                lista[index] = pedido;
                await _context.SaveAsync(FileName, lista);
            }
        }

        public async Task DeleteAsync(PedidoEntity pedido)
        {
            var lista = await _context.GetAsync<PedidoEntity>(FileName);
            var item = lista.FirstOrDefault(p => p.IdPedido == pedido.IdPedido);

            if (item != null)
            {
                lista.Remove(item);
                await _context.SaveAsync(FileName, lista);
            }
        }
    }
}
