namespace AppEcommerce.Domain.Entities
{
    public class ItemPedidoEntity
    {
        public int IdProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Subtotal { get; private set; }

        public ItemPedidoEntity() { }

        public ItemPedidoEntity(int idProduto, int quantidade, decimal preco)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade inválida.");

            IdProduto = idProduto;
            Quantidade = quantidade;
            Subtotal = preco * quantidade;
        }

        public void UpdateQuantidade(int quantidade, decimal preco)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade inválida.");

            Quantidade = quantidade;
            Subtotal = quantidade * preco;
        }
    }
}
