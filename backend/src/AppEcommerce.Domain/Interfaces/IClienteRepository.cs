using AppEcommerce.Domain.Entities;

namespace AppEcommerce.Domain.Interfaces;

public interface IClienteRepository
{
    Task AddAsync(ClienteEntity cliente);
    Task UpdateAsync(ClienteEntity cliente);
    Task DeleteAsync(int id);
    Task<IEnumerable<ClienteEntity>> GetAllAsync();
    Task<ClienteEntity?> GetByIdAsync(int id);
    Task<ClienteEntity?> GetByEmailAsync(string email);
}