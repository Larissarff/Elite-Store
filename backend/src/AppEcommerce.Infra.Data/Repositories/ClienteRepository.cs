using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;
using AppEcommerce.Infra.Data.Context;

namespace AppEcommerce.Infra.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly JsonContext _context;
    private const string FileName = "clientes.json";

    public ClienteRepository(JsonContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ClienteEntity cliente)
    {
        var lista = await _context.GetAsync<ClienteEntity>(FileName);
        
        int novoId = lista.Any() ? lista.Max(c => c.Id) + 1 : 1;
        cliente.DefinirId(novoId);
        
        lista.Add(cliente);
        await _context.SaveAsync(FileName, lista);
    }

    public async Task UpdateAsync(ClienteEntity cliente)
    {
        var lista = await _context.GetAsync<ClienteEntity>(FileName);
        var index = lista.FindIndex(c => c.Id == cliente.Id);
        
        if (index != -1)
        {
            lista[index] = cliente;
            await _context.SaveAsync(FileName, lista);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var lista = await _context.GetAsync<ClienteEntity>(FileName);
        var item = lista.FirstOrDefault(c => c.Id == id);
        
        if (item != null)
        {
            lista.Remove(item);
            await _context.SaveAsync(FileName, lista);
        }
    }

    public async Task<IEnumerable<ClienteEntity>> GetAllAsync()
        => await _context.GetAsync<ClienteEntity>(FileName);

    public async Task<ClienteEntity?> GetByIdAsync(int id)
    {
        var lista = await _context.GetAsync<ClienteEntity>(FileName);
        return lista.FirstOrDefault(c => c.Id == id);
    }

    public async Task<ClienteEntity?> GetByEmailAsync(string email)
    {
        var lista = await _context.GetAsync<ClienteEntity>(FileName);
        return lista.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}