using System.Text.Json.Serialization;
using AppEcommerce.Domain.Enums;

namespace AppEcommerce.Domain.Entities;

public class ProdutoEntity
{
    [JsonInclude]
    public int Id { get; private set; }

    [JsonInclude]
    public string Nome { get; private set; } = string.Empty;

    [JsonInclude]
    public string Descricao { get; private set; } = string.Empty;

    [JsonInclude]
    public decimal Preco { get; private set; }

    [JsonInclude]
    public TamanhoEnum Tamanho { get; private set; }

    [JsonInclude]
    public int Estoque { get; private set; }

    [JsonInclude]
    public string Categoria { get; private set; } = string.Empty;

    public ProdutoEntity() { }

    public ProdutoEntity(string nome, string descricao, decimal preco, TamanhoEnum tamanho, int estoque, string categoria)
    {
        ValidateDomain(nome, descricao, preco, estoque, categoria);
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Tamanho = tamanho;
        Estoque = estoque;
        Categoria = categoria;
    }

    public void DefinirId(int id)
    {
        Id = id;
    }

    public void Update(string nome, string descricao, decimal preco, TamanhoEnum tamanho, int estoque, string categoria)
    {
        ValidateDomain(nome, descricao, preco, estoque, categoria);
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Tamanho = tamanho;
        Estoque = estoque;
        Categoria = categoria;
    }

    public void DebitarEstoque(int quantidade)
    {
        if (quantidade > Estoque)
            throw new Exception($"Estoque insuficiente. Disponível: {Estoque}");
        Estoque -= quantidade;
    }

    private void ValidateDomain(string nome, string descricao, decimal preco, int estoque, string categoria)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException("Descrição é obrigatória.");
        if (preco <= 0) throw new ArgumentException("Preço deve ser maior que zero.");
        if (estoque < 0) throw new ArgumentException("Estoque não pode ser negativo.");
        if (string.IsNullOrWhiteSpace(categoria)) throw new ArgumentException("Categoria é obrigatória.");
    }
}