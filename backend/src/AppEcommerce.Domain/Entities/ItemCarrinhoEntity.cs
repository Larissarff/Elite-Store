namespace AppEcommerce.Domain.Entities;

public class ItemCarrinhoEntity
{
    public int ItemCarrinhoId { get; set; }
    public int CarrinhoId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; private set; }

    public ItemCarrinhoEntity() { }

    public ItemCarrinhoEntity(int carrinhoId, int produtoId, int quantidade, decimal precoUnitario)
    {
        CarrinhoId = carrinhoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Subtotal = CalcularSubtotal();
    }

    public void Update(int produtoId, int quantidade, decimal precoUnitario)
    {
        if (produtoId <= 0)
            throw new ArgumentException("Id do produto inválido.");

        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Subtotal = CalcularSubtotal();
    }

    public decimal CalcularSubtotal()
    {
        return Quantidade * PrecoUnitario;
    }
}