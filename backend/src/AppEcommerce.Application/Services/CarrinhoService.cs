using AppEcommerce.Application.DTOs.Carrinho;
using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        public async Task<CarrinhoDto?> GetByIdAsync(int id)
        {
            var carrinho = await _carrinhoRepository.GetByIdAsync(id);
            return carrinho is null ? null : MapToDto(carrinho);
        }

        public async Task<IEnumerable<CarrinhoDto>> GetAllAsync()
        {
            var carrinhos = await _carrinhoRepository.GetAllAsync();
            return carrinhos.Select(MapToDto);
        }

        public async Task<CarrinhoDto> CreateAsync(CreateCarrinhoDto dto)
        {
            if (dto.IdCarrinho <= 0)
                throw new ArgumentException("Id do carrinho deve ser maior que zero.", nameof(dto.IdCarrinho));

            if (dto.IdCliente <= 0)
                throw new ArgumentException("Id do cliente deve ser maior que zero.", nameof(dto.IdCliente));

            var existente = await _carrinhoRepository.GetByIdAsync(dto.IdCarrinho);
            if (existente is not null)
                throw new InvalidOperationException($"Já existe carrinho com Id {dto.IdCarrinho}.");

            var carrinho = new CarrinhoEntity(dto.IdCarrinho, dto.IdCliente);

            await _carrinhoRepository.AddAsync(carrinho);

            return MapToDto(carrinho);
        }

        public async Task<CarrinhoDto> AddItemAsync(int carrinhoId, AddItemCarrinhoDto dto)
        {
            var carrinho = await _carrinhoRepository.GetByIdAsync(carrinhoId)
                           ?? throw new KeyNotFoundException($"Carrinho {carrinhoId} não encontrado.");

            if (dto.IdProduto <= 0)
                throw new ArgumentException("Id do produto inválido.", nameof(dto.IdProduto));

            if (dto.Quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.", nameof(dto.Quantidade));

            if (dto.PrecoUnitario < 0)
                throw new ArgumentException("Preço unitário não pode ser negativo.", nameof(dto.PrecoUnitario));

            carrinho.AdicionarItem(dto.IdProduto, dto.Quantidade, dto.PrecoUnitario);

            await _carrinhoRepository.UpdateAsync(carrinho);

            return MapToDto(carrinho);
        }

        public async Task<CarrinhoDto> RemoveItemAsync(int carrinhoId, int idProduto)
        {
            var carrinho = await _carrinhoRepository.GetByIdAsync(carrinhoId)
                           ?? throw new KeyNotFoundException($"Carrinho {carrinhoId} não encontrado.");

            carrinho.RemoverItem(idProduto);

            await _carrinhoRepository.UpdateAsync(carrinho);

            return MapToDto(carrinho);
        }

        public async Task DeleteAsync(int carrinhoId)
        {
            var carrinho = await _carrinhoRepository.GetByIdAsync(carrinhoId);

            if (carrinho is null)
                throw new KeyNotFoundException($"Carrinho {carrinhoId} não encontrado.");

            await _carrinhoRepository.DeleteAsync(carrinhoId);
        }

        // Mapeia CarrinhoEntity -> CarrinhoDto
        private static CarrinhoDto MapToDto(CarrinhoEntity carrinho)
        {
            var dto = new CarrinhoDto
            {
                IdCarrinho = carrinho.IdCarrinho,
                IdCliente = carrinho.IdCliente,
                Total = carrinho.CalcularTotal()
            };

            if (carrinho.Itens is not null)
            {
                dto.Itens = carrinho.Itens
                    .Select(i => new ItemCarrinhoResumoDto
                    {
                        IDProduto = i.IDProduto,
                        Quantidade = i.Quantidade,
                        Subtotal = i.Subtotal
                    })
                    .ToList();
            }

            return dto;
        }
    }
}
