using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories
{
    public class ItemCarrinhoRepository : IItemCarrinhoRepository
    {
        private readonly JsonContext _json;
        private const string FILE_NAME = "itens_carrinho.json";

        public ItemCarrinhoRepository(JsonContext json)
        {
            _json = json;
        }

        public async Task<ItemCarrinhoEntity?> GetByIdAsync(int id)
        {
            var itens = await _json.GetAsync<ItemCarrinhoEntity>(FILE_NAME);
            return itens.FirstOrDefault(x => x.ItemCarrinhoId == id);
        }

        public async Task<IEnumerable<ItemCarrinhoEntity>> GetAllAsync()
        {
            return await _json.GetAsync<ItemCarrinhoEntity>(FILE_NAME);
        }

        public async Task AddAsync(ItemCarrinhoEntity entity)
        {
            var itens = await _json.GetAsync<ItemCarrinhoEntity>(FILE_NAME);

            // Geração automática de ID
            entity.ItemCarrinhoId = itens.Count == 0
                ? 1
                : itens.Max(x => x.ItemCarrinhoId) + 1;

            itens.Add(entity);
            await _json.SaveAsync(FILE_NAME, itens);
        }

        public async Task UpdateAsync(ItemCarrinhoEntity entity)
        {
            var itens = await _json.GetAsync<ItemCarrinhoEntity>(FILE_NAME);
            var index = itens.FindIndex(x => x.ItemCarrinhoId == entity.ItemCarrinhoId);

            if (index == -1)
                throw new KeyNotFoundException(
                    $"ItemCarrinho com ID {entity.ItemCarrinhoId} não encontrado.");

            itens[index] = entity;
            await _json.SaveAsync(FILE_NAME, itens);
        }

        public async Task DeleteAsync(int id)
        {
            var itens = await _json.GetAsync<ItemCarrinhoEntity>(FILE_NAME);
            var entity = itens.FirstOrDefault(x => x.ItemCarrinhoId == id);

            if (entity is null)
                throw new KeyNotFoundException(
                    $"ItemCarrinho com ID {id} não encontrado.");

            itens.Remove(entity);
            await _json.SaveAsync(FILE_NAME, itens);
        }
    }
}
