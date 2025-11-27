using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppEcommerce.Infra.Data.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly AppDbContext _ctx;

    public PagamentoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(PagamentoEntity pagamento)
    {
        await _ctx.Pagamentos.AddAsync(pagamento);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(PagamentoEntity pagamento)
    {
        _ctx.Pagamentos.Remove(pagamento);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<PagamentoEntity>> GetAllAsync()
        => await _ctx.Pagamentos.AsNoTracking().ToListAsync();

    public async Task<PagamentoEntity?> GetByIdAsync(int id)
        => await _ctx.Pagamentos.FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdateAsync(PagamentoEntity pagamento)
    {
        _ctx.Pagamentos.Update(pagamento);
        await _ctx.SaveChangesAsync();
    }
}
