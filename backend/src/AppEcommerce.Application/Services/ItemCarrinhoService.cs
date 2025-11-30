using AppEcommerce.Application.DTOs.ItemCarrinho;
using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services
{
    public class ItemCarrinhoService : IItemCarrinhoService
    {
        private readonly IItemCarrinhoRepository _itemCarrinhoRepository;

        public ItemCarrinhoService(IItemCarrinhoRepository itemCarrinhoRepository)
        {
            _itemCarrinhoRepository = itemCarrinhoRepository;
        }

        public async Task<ItemCarrinhoDto?> GetByIdAsync(int id)
        {
            var entity = await _itemCarrinhoRepository.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<IEnumerable<ItemCarrinhoDto>> GetAllAsync()
        {
            var itens = await _itemCarrinhoRepository.GetAllAsync();
            return itens.Select(MapToDto);
        }

        public async Task<IEnumerable<ItemCarrinhoDto>> GetByCarrinhoAsync(int carrinhoId)
        {
            var itens = await _itemCarrinhoRepository.GetAllAsync();
            return itens
                .Where(i => i.CarrinhoId == carrinhoId)
                .Select(MapToDto);
        }

        public async Task<ItemCarrinhoDto> AddAsync(CreateItemCarrinhoDto dto)
        {
            ValidarItem(dto.Quantidade, dto.PrecoUnitario);

            var entity = new ItemCarrinhoEntity
            {
                CarrinhoId = dto.CarrinhoId,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade,
                PrecoUnitario = dto.PrecoUnitario
            };

            await _itemCarrinhoRepository.AddAsync(entity);

            return MapToDto(entity);
        }

        public async Task<ItemCarrinhoDto> UpdateAsync(int id, UpdateItemCarrinhoDto dto)
        {
            ValidarItem(dto.Quantidade, dto.PrecoUnitario);

            var existente = await _itemCarrinhoRepository.GetByIdAsync(id);

            if (existente is null)
                throw new KeyNotFoundException($"Item de carrinho {id} não encontrado.");

            existente.Quantidade = dto.Quantidade;
            existente.PrecoUnitario = dto.PrecoUnitario;

            await _itemCarrinhoRepository.UpdateAsync(existente);

            return MapToDto(existente);
        }

        public async Task DeleteAsync(int id)
        {
            var existente = await _itemCarrinhoRepository.GetByIdAsync(id);

            if (existente is null)
                throw new KeyNotFoundException($"Item de carrinho {id} não encontrado.");

            await _itemCarrinhoRepository.DeleteAsync(id);
        }

        private static void ValidarItem(int quantidade, decimal precoUnitario)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.", nameof(quantidade));

            if (precoUnitario < 0)
                throw new ArgumentException("Preço unitário não pode ser negativo.", nameof(precoUnitario));
        }

        private static ItemCarrinhoDto MapToDto(ItemCarrinhoEntity entity)
        {
            return new ItemCarrinhoDto
            {
                ItemCarrinhoId = entity.ItemCarrinhoId,
                CarrinhoId = entity.CarrinhoId,
                ProdutoId = entity.ProdutoId,
                Quantidade = entity.Quantidade,
                PrecoUnitario = entity.PrecoUnitario,
                Subtotal = entity.CalcularSubtotal()
            };
        }
    }
}
