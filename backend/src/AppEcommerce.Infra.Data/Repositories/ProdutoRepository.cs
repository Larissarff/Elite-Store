using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppEcommerce.Infra.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _ctx;

    public ProdutoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(ProdutoEntity produto)
    {
        await _ctx.ProdutoEntitys.AddAsync(produto);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProdutoEntity produto)
    {
        _ctx.ProdutoEntitys.Remove(produto);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProdutoEntity>> GetAllAsync()
        => await _ctx.ProdutoEntitys.AsNoTracking().ToListAsync();

    public async Task<ProdutoEntity?> GetByIdAsync(int id)
        => await _ctx.ProdutoEntitys.FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdateAsync(ProdutoEntity ProdutoEntity)
    {
        _ctx.ProdutoEntitys.Update(ProdutoEntity);
        await _ctx.SaveChangesAsync();
    }
}
