using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly JsonContext _json;
        private const string FILE_NAME = "itens_pedido.json";

        public ItemPedidoRepository(JsonContext json)
        {
            _json = json;
        }

        public async Task AddAsync(ItemPedidoEntity item)
        {
            var lista = await _json.GetAsync<ItemPedidoEntity>(FILE_NAME);

            // Não há ID no ItemPedidoEntity atualmente, então apenas adicionamos
            lista.Add(item);
            await _json.SaveAsync(FILE_NAME, lista);
        }

        public async Task DeleteAsync(ItemPedidoEntity item)
        {
            var lista = await _json.GetAsync<ItemPedidoEntity>(FILE_NAME);
            var existente = lista.FirstOrDefault(i => i.IdProduto == item.IdProduto && i.Quantidade == item.Quantidade && i.Subtotal == item.Subtotal);
            if (existente != null)
            {
                lista.Remove(existente);
                await _json.SaveAsync(FILE_NAME, lista);
            }
        }

        public async Task<IEnumerable<ItemPedidoEntity>> GetAllAsync()
            => await _json.GetAsync<ItemPedidoEntity>(FILE_NAME);

        public async Task<ItemPedidoEntity?> GetByIdAsync(int id)
        {
            var lista = await _json.GetAsync<ItemPedidoEntity>(FILE_NAME);
            // ItemPedidoEntity não possui Id explícito; procurar por correspondência baseada no índice/Produto
            return lista.ElementAtOrDefault(id - 1);
        }

        public async Task UpdateAsync(ItemPedidoEntity item)
        {
            var lista = await _json.GetAsync<ItemPedidoEntity>(FILE_NAME);
            var index = lista.FindIndex(i => i.IdProduto == item.IdProduto && i.Subtotal == item.Subtotal && i.Quantidade == item.Quantidade);
            if (index != -1)
            {
                lista[index] = item;
                await _json.SaveAsync(FILE_NAME, lista);
            }
        }
    }
}