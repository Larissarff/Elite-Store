using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;

        public ProdutoService(IProdutoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProdutoEntity>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<ProdutoEntity?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(ProdutoEntity produto)
        {
            await _repo.AddAsync(produto);
        }

        public async Task UpdateAsync(ProdutoEntity produto)
        {
            await _repo.UpdateAsync(produto);
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
