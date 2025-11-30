namespace AppEcommerce.Domain.Entities;

public class CarrinhoEntity 
{
    public int IdCarrinho {get; private set;}
    public int IdCliente {get; private set;} 
    public List<ItemCarrinhoEntity> Itens {get; private set;} = new List<ItemCarrinhoEntity>();

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
        var itemExistente = Itens.FirstOrDefault(i => i.ProdutoId == idProduto);
        if (itemExistente != null)
        {
            var novaQuantidade = itemExistente.Quantidade + quantidade;
            itemExistente.Update(itemExistente.ProdutoId, novaQuantidade, precoUnitario);
        }
        else
        {
            Itens.Add(new ItemCarrinhoEntity(IdCarrinho, idProduto, quantidade, precoUnitario));
        }
    }

    public void RemoverItem(int idProduto)
    {
        Itens.RemoveAll(i => i.ProdutoId == idProduto);
    }

    public decimal CalcularTotal()
    {
        return Itens.Sum(i => i.Subtotal);
    }
}


