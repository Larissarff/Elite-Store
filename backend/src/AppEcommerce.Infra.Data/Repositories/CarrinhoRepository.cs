using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly JsonContext _json;
        private const string FILE_NAME = "carrinhos.json";

        public CarrinhoRepository(JsonContext json)
        {
            _json = json;
        }

        public async Task<CarrinhoEntity?> GetByIdAsync(int id)
        {
            var carrinhos = await _json.GetAsync<CarrinhoEntity>(FILE_NAME);
            return carrinhos.FirstOrDefault(c => c.IdCarrinho == id);
        }

        public async Task<IEnumerable<CarrinhoEntity>> GetAllAsync()
        {
            return await _json.GetAsync<CarrinhoEntity>(FILE_NAME);
        }

        public async Task AddAsync(CarrinhoEntity carrinho)
        {
            var carrinhos = await _json.GetAsync<CarrinhoEntity>(FILE_NAME);

            if (carrinhos.Any(c => c.IdCarrinho == carrinho.IdCarrinho))
                throw new InvalidOperationException(
                    $"Já existe um carrinho com Id {carrinho.IdCarrinho}.");

            carrinhos.Add(carrinho);
            await _json.SaveAsync(FILE_NAME, carrinhos);
        }

        public async Task UpdateAsync(CarrinhoEntity carrinho)
        {
            var carrinhos = await _json.GetAsync<CarrinhoEntity>(FILE_NAME);
            var index = carrinhos.FindIndex(c => c.IdCarrinho == carrinho.IdCarrinho);

            if (index == -1)
                throw new KeyNotFoundException(
                    $"Carrinho com Id {carrinho.IdCarrinho} não encontrado.");

            carrinhos[index] = carrinho;
            await _json.SaveAsync(FILE_NAME, carrinhos);
        }

        public async Task DeleteAsync(int id)
        {
            var carrinhos = await _json.GetAsync<CarrinhoEntity>(FILE_NAME);
            var carrinho = carrinhos.FirstOrDefault(c => c.IdCarrinho == id);

            if (carrinho is null)
                throw new KeyNotFoundException(
                    $"Carrinho com Id {id} não encontrado.");

            carrinhos.Remove(carrinho);
            await _json.SaveAsync(FILE_NAME, carrinhos);
        }
    }
}
