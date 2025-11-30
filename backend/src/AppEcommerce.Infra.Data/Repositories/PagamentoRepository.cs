using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly JsonContext _context;
    private const string FileName = "pagamentos.json";

    public PagamentoRepository(JsonContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PagamentoEntity pagamento)
    {
        var lista = await _context.GetAsync<PagamentoEntity>(FileName);

        // Lógica de Auto-Increment manual (Id = Max + 1)
        int novoId = lista.Any() ? lista.Max(p => p.Id) + 1 : 1;
        
        // Assumindo que você criou o método DefinirId na entidade (igual fizemos no Cliente/Produto)
        pagamento.DefinirId(novoId);

        lista.Add(pagamento);
        await _context.SaveAsync(FileName, lista);
    }

    public async Task DeleteAsync(PagamentoEntity pagamento)
    {
        var lista = await _context.GetAsync<PagamentoEntity>(FileName);
        
        // Buscamos pelo ID para garantir a remoção correta
        var itemParaRemover = lista.FirstOrDefault(p => p.Id == pagamento.Id);
        
        if (itemParaRemover != null)
        {
            lista.Remove(itemParaRemover);
            await _context.SaveAsync(FileName, lista);
        }
    }

    public async Task<IEnumerable<PagamentoEntity>> GetAllAsync()
    {
        return await _context.GetAsync<PagamentoEntity>(FileName);
    }

    public async Task<PagamentoEntity?> GetByIdAsync(int id)
    {
        var lista = await _context.GetAsync<PagamentoEntity>(FileName);
        return lista.FirstOrDefault(p => p.Id == id);
    }

    public async Task UpdateAsync(PagamentoEntity pagamento)
    {
        var lista = await _context.GetAsync<PagamentoEntity>(FileName);
        
        var index = lista.FindIndex(p => p.Id == pagamento.Id);

        if (index != -1)
        {
            lista[index] = pagamento;
            await _context.SaveAsync(FileName, lista);
        }
    }
}