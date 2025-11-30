using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppEcommerce.Infra.Data.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly AppDbContext _ctx;

        public ItemPedidoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(ItemPedidoEntity item)
        {
            await _ctx.ItensPedido.AddAsync(item);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(ItemPedidoEntity item)
        {
            _ctx.ItensPedido.Remove(item);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemPedidoEntity>> GetAllAsync()
            => await _ctx.ItensPedido.AsNoTracking().ToListAsync();

        public async Task<ItemPedidoEntity?> GetByIdAsync(int id)
            => await _ctx.ItensPedido
                         .AsNoTracking()
                         .FirstOrDefaultAsync(i => i.IdItemPedido == id);

        public async Task UpdateAsync(ItemPedidoEntity item)
        {
            _ctx.ItensPedido.Update(item);
            await _ctx.SaveChangesAsync();
        }
    }
}