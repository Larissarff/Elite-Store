using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppEcommerce.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _ctx;

        public PedidoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(PedidoEntity pedido)
        {
            await _ctx.Pedidos.AddAsync(pedido);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(PedidoEntity pedido)
        {
            _ctx.Pedidos.Remove(pedido);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllAsync()
            => await _ctx.Pedidos.AsNoTracking().ToListAsync();

        public async Task<PedidoEntity?> GetByIdAsync(int id)
            => await _ctx.Pedidos
                         .Include(p => p.Itens)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(p => p.IdPedido == id);

        public async Task UpdateAsync(PedidoEntity pedido)
        {
            _ctx.Pedidos.Update(pedido);
            await _ctx.SaveChangesAsync();
        }
    }
}
