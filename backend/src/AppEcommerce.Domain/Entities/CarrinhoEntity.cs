namespace AppEcommerce.Domain.Entities;

public class CarrinhoEntity 
{
    public int IdCarrinho {get; private set;}
    public int IdCliente {get; private set;} 
    public List<ItemCarrinhoEntity> Itens {get; private set;}

    protected CarrinhoEntity () { }

    public CarrinhoEntity (int idcarrinho, int idcliente) 
    { 
        Update(idcarrinho, idcliente); 
    }

    public void Update(int idcarrinho, int idcliente)
    {
        if (idcarrinho <= 0)
                throw new ArgumentException("Id do carrinho inválido.");

        if (idcliente <= 0)
                throw new ArgumentException("Id do cliente inválido.");

        IdCarrinho = idcarrinho;
        IdCliente = idcliente;
        Itens = new List<ItemCarrinhoEntity>();    
    }

    public void AdicionarItem(int idProduto, int quantidade, decimal precoUnitario)
    {
        var itemExistente = Itens.FirstOrDefault(i => i.IDProduto == idProduto);
        if (itemExistente != null)
        {
            itemExistente.Quantidade += quantidade;
            itemExistente.Subtotal = itemExistente.Quantidade * precoUnitario;
        }
        else
        {
            Itens.Add(new ItemCarrinhoEntity(idProduto, quantidade, precoUnitario));
        }
    }

    public void RemoverItem(int idProduto)
    {
        Itens.RemoveAll(i => i.IDProduto == idProduto);
    }

    public decimal CalcularTotal()
    {
        return Itens.Sum(i => i.Subtotal);
    }
}


