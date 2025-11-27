using AppEcommerce.Domain.Enums;

namespace AppEcommerce.Domain.Entities;

public class ProdutoEntity
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public decimal Preco { get; private set; }
    public TamanhoEnum Tamanho { get; private set; }
    public int Estoque { get; private set; } = 0;
    public string Categoria { get; private set; } = string.Empty;

    protected ProdutoEntity() { } 

    public ProdutoEntity(string nome, string descricao, decimal preco, TamanhoEnum tamanho, int estoque, string categoria)
        => Update(nome, descricao, preco, tamanho, estoque, categoria);

    public void Update(string nome, string descricao, decimal preco, TamanhoEnum tamanho, int estoque, string categoria)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");
        if (preco < 0)
            throw new ArgumentException("Preço não pode ser negativo.");
        if (estoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo.");
        if (string.IsNullOrWhiteSpace(categoria))
            throw new ArgumentException("Categoria é obrigatória.");

        Nome = nome.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        Preco = decimal.Round(preco, 2); // formatação
        Tamanho = tamanho;
        Estoque = estoque;
        Categoria = categoria.Trim();
    }

    public void DebitarEstoque(int qtd)
    {
        if (qtd <= 0) 
           throw new ArgumentException("Quantidade inválida.");
        if (qtd > Estoque)
           throw new InvalidOperationException("Estoque insuficiente.");
        Estoque -= qtd;
    }

    public void ReporEstoque(int qtd)
    {
        if (qtd <= 0)
            throw new ArgumentException("Quantidade inválida.");
        Estoque += qtd;
    }
}
