namespace AppEcommerce.Domain.Entities
{
    public class ItemPedidoEntity
    {
        public int IdItemPedido { get; private set; }
        public int IdPedido { get; private set; }
        public int IdProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Subtotal { get; private set; }

        public PedidoEntity Pedido { get; private set; }

        protected ItemPedidoEntity() { }

        public ItemPedidoEntity(int idProduto, int quantidade, decimal subtotal)
        {
            Update(idProduto, quantidade, subtotal);
        }

        public void Update(int idProduto, int quantidade, decimal subtotal)
        {
            if (idProduto <= 0)
                throw new ArgumentException("Id do produto inválido.");

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            if (subtotal < 0)
                throw new ArgumentException("Subtotal inválido.");

            IdProduto = idProduto;
            Quantidade = quantidade;
            Subtotal = subtotal;
        }
    }
}