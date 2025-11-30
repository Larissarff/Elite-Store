namespace AppEcommerce.Domain.Entities;

public class ItemCarrinhoEntity
{
    public int ItemCarrinhoId { get; set; }
    public int IDProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal { get; set; }

    protected ItemCarrinhoEntity () { }

     public ItemCarrinhoEntity (int idcarrinho, int idcliente, decimal precoUnitario) 
    { 
        Update(idcarrinho, idcliente, precoUnitario); 
    }
    public void Update(int idProduto, int quantidade, decimal precoUnitario)
    {
        if (idProduto <= 0)
                throw new ArgumentException("Id do produto inválido.");

        IDProduto = idProduto;
        Quantidade = quantidade;
        Subtotal = quantidade * precoUnitario;
    }

}