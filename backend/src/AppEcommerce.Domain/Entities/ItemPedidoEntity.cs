namespace AppEcommerce.Domain.Entities
{
    public class ItemPedidoEntity
    {
        public int IdProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Subtotal { get; private set; }

        protected ItemPedidoEntity() { }

        public ItemPedidoEntity(int idProduto, int quantidade, decimal preco)
        {
            Update(idProduto, quantidade, preco);
        }

        public void Update(int idProduto, int quantidade, decimal preco)
        {
            if (idProduto <= 0)
                throw new ArgumentException("Id do produto inválido.");

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            if (preco < 0)
                throw new ArgumentException("Preço inválido.");

            IdProduto = idProduto;
            Quantidade = quantidade;
            Subtotal = quantidade * preco;
        }
    }
}
