namespace AppEcommerce.Domain.Entities
{
    public class PedidoEntity
    {
        public int IdPedido { get; private set; }
        public int IdCliente { get; private set; }
        public DateTime DataPedido { get; private set; }
        public decimal ValorTotal { get; private set; }

        public List<ItemPedidoEntity> Itens { get; private set; } = new();

        protected PedidoEntity() { }

        public PedidoEntity(int idCliente)
        {
            Update(idCliente);
            DataPedido = DateTime.UtcNow;
            ValorTotal = 0;
        }

        public void Update(int idCliente)
        {
            if (idCliente <= 0)
                throw new ArgumentException("Id do cliente inválido.");

            IdCliente = idCliente;
        }

        public void AdicionarItem(ItemPedidoEntity item)
        {
            if (item == null)
                throw new ArgumentException("Item inválido.");

            Itens.Add(item);
            RecalcularTotal();
        }

        public void RemoverItem(ItemPedidoEntity item)
        {
            if (item == null)
                throw new ArgumentException("Item inválido.");

            Itens.Remove(item);
            RecalcularTotal();
        }

        private void RecalcularTotal()
        {
            ValorTotal = Itens.Sum(i => i.Subtotal);
        }

        public void DefinirId(int id)
        {
            IdPedido = id;
        }
    }
}