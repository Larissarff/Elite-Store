using AppEcommerce.Application.DTOs.ItemCarrinho;

namespace AppEcommerce.Application.Interfaces
{
    public interface IItemCarrinhoService
    {
        Task<ItemCarrinhoDto?> GetByIdAsync(int id);
        Task<IEnumerable<ItemCarrinhoDto>> GetAllAsync();
        Task<IEnumerable<ItemCarrinhoDto>> GetByCarrinhoAsync(int carrinhoId);

        Task<ItemCarrinhoDto> AddAsync(CreateItemCarrinhoDto dto);
        Task<ItemCarrinhoDto> UpdateAsync(int id, UpdateItemCarrinhoDto dto);
        Task DeleteAsync(int id);
    }
}
