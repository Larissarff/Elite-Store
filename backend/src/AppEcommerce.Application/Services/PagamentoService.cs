using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services;

public class PagamentoService
{
    private readonly IPagamentoRepository _repo;

    public PagamentoService(IPagamentoRepository repo)
    {
        _repo = repo;
    }

    // Buscar todos os pagamentos
    public async Task<IEnumerable<PagamentoEntity>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    // Buscar pagamento por ID
    public async Task<PagamentoEntity?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    // Adicionar novo pagamento
    public async Task AddAsync(PagamentoEntity pagamento)
    {
        await _repo.AddAsync(pagamento);
    }

    // Atualizar pagamento existente
    public async Task UpdateAsync(PagamentoEntity pagamento)
    {
        await _repo.UpdateAsync(pagamento);
    }

    // Deletar pagamento
    public async Task DeleteAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is not null)
        {
            await _repo.DeleteAsync(existing);
        }
    }
}
