using AppEcommerce.Application.DTOs;
using AppEcommerce.Application.Requests;
using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Interfaces;

namespace AppEcommerce.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClienteDto> CreateAsync(CreateClienteRequest request)
    {
        var existente = await _repository.GetByEmailAsync(request.Email);
        if (existente != null)
            throw new Exception("E-mail já cadastrado.");

        var entity = new ClienteEntity(request.Nome, request.Email, request.Senha, request.Endereco);
        await _repository.AddAsync(entity);

        return new ClienteDto 
        { 
            Id = entity.Id, 
            Nome = entity.Nome, 
            Email = entity.Email, 
            Endereco = entity.Endereco 
        };
    }

    public async Task UpdateAsync(UpdateClienteRequest request)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) throw new Exception("Cliente não encontrado.");

        entity.Update(request.Nome, request.Email, request.Senha, request.Endereco);

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(c => new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Email = c.Email,
            Endereco = c.Endereco
        });
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;

        return new ClienteDto
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Email = entity.Email,
            Endereco = entity.Endereco
        };
    }
}