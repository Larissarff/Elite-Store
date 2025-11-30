using System.Text.Json.Serialization;

namespace AppEcommerce.Domain.Entities;

public class ClienteEntity
{
    [JsonInclude]
    public int Id { get; private set; }

    [JsonInclude]
    public string Nome { get; private set; } = string.Empty;

    [JsonInclude]
    public string Email { get; private set; } = string.Empty;

    [JsonInclude]
    public string Senha { get; private set; } = string.Empty;

    [JsonInclude]
    public string Endereco { get; private set; } = string.Empty;

    public ClienteEntity() { }

    public ClienteEntity(string nome, string email, string senha, string endereco)
    {
        ValidateDomain(nome, email, senha, endereco);
        Nome = nome;
        Email = email;
        Senha = senha;
        Endereco = endereco;
    }

    public void DefinirId(int id) => Id = id;

    public void Update(string nome, string email, string senha, string endereco)
    {
        ValidateDomain(nome, email, senha, endereco);
        Nome = nome;
        Email = email;
        Senha = senha;
        Endereco = endereco;
    }

    private void ValidateDomain(string nome, string email, string senha, string endereco)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.");
        if (string.IsNullOrWhiteSpace(senha)) throw new ArgumentException("Senha é obrigatória.");
        if (string.IsNullOrWhiteSpace(endereco)) throw new ArgumentException("Endereço é obrigatório.");
    }
}