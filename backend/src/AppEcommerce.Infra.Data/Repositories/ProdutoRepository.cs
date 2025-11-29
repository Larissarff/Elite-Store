using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly JsonContext _context;
    private const string FileName = "produtos.json";

    public ProdutoRepository(JsonContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProdutoEntity produto)
    {
        var lista = await _context.GetAsync<ProdutoEntity>(FileName);

        int novoId = lista.Any() ? lista.Max(p => p.Id) + 1 : 1;
        
        // CORREÇÃO AQUI: Usando o método auxiliar em vez de atribuir direto
        produto.DefinirId(novoId);

        lista.Add(produto);
        await _context.SaveAsync(FileName, lista);
    }

    public async Task DeleteAsync(ProdutoEntity produto)
    {
        // Precisamos buscar pelo ID para garantir que estamos removendo o objeto da lista atual em memória
        // pois o objeto 'produto' que chega por parâmetro pode ser uma instância diferente
        var lista = await _context.GetAsync<ProdutoEntity>(FileName);
        
        var itemParaRemover = lista.FirstOrDefault(p => p.Id == produto.Id);
        
        if (itemParaRemover != null)
        {
            lista.Remove(itemParaRemover);
            await _context.SaveAsync(FileName, lista);
        }
    }

    public async Task<IEnumerable<ProdutoEntity>> GetAllAsync()
    {
        return await _context.GetAsync<ProdutoEntity>(FileName);
    }

    public async Task<ProdutoEntity?> GetByIdAsync(int id)
    {
        var lista = await _context.GetAsync<ProdutoEntity>(FileName);
        return lista.FirstOrDefault(p => p.Id == id);
    }

    public async Task UpdateAsync(ProdutoEntity produto)
    {
        var lista = await _context.GetAsync<ProdutoEntity>(FileName);
        
        var index = lista.FindIndex(p => p.Id == produto.Id);
        
        if (index != -1)
        {
            lista[index] = produto; 
            await _context.SaveAsync(FileName, lista);
        }
    }
}