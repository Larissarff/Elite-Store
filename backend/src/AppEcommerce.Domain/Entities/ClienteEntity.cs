using System.Data;

namespace AppEcommerce.Domain.Entities;

public class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public string Endereco { get; private set; } = string.Empty;

    protected Cliente() { }

    public Cliente(string nome, string email, string senha, string endereco)
        => Update(nome, email, senha, endereco);

    public void Update(string nome, string email, string senha, string endereco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email é obrigatório.");
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha é obrigatória.");
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("Endereço é obrigatório.");

        Nome = nome.Trim();
        Email = email.Trim();
        Senha = senha.Trim();
        Endereco = endereco.Trim();
    }

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("Nome é obrigatório");
        Email = novoNome.Trim();
    }

    public void AlterarEmail(string novoEmail)
    {
        if (string.IsNullOrWhiteSpace(novoEmail))
            throw new ArgumentException("Email é obrigatório");
        Email = novoEmail.Trim();
    }

    public void AlterarSenha(string novaSenha)
    {
        if (string.IsNullOrWhiteSpace(novaSenha))
            throw new ArgumentException("Senha é obrigatória.");
        Senha = novaSenha.Trim();
    }

    public void AlterarEndereco(string novoEndereco)
    {
        if (string.IsNullOrWhiteSpace(novoEndereco))
            throw new ArgumentException("Endereço é obrigatório");
        Endereco = novoEndereco.Trim();
    }
}