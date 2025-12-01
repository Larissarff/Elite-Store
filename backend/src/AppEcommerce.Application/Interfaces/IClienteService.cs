using AppEcommerce.Application.DTOs;
using AppEcommerce.Application.Requests;

namespace AppEcommerce.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> CreateAsync(CreateClienteRequest request);
        Task UpdateAsync(UpdateClienteRequest request);
        Task DeleteAsync(int id);
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync(int id);
    }
}
