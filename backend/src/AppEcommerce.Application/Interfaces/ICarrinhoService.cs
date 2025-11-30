using AppEcommerce.Application.DTOs.Carrinho;

namespace AppEcommerce.Application.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoDto?> GetByIdAsync(int id);
        Task<IEnumerable<CarrinhoDto>> GetAllAsync();

        Task<CarrinhoDto> CreateAsync(CreateCarrinhoDto dto);
        Task<CarrinhoDto> AddItemAsync(int carrinhoId, AddItemCarrinhoDto dto);
        Task<CarrinhoDto> RemoveItemAsync(int carrinhoId, int idProduto);

        Task DeleteAsync(int carrinhoId);
    }
}
