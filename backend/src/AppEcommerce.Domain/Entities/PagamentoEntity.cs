using AppEcommerce.Domain.Enums;

namespace AppEcommerce.Domain.Entities
{
    public class PagamentoEntity
    {
        public int Id { get; private set; }
        public int IdPedido { get; private set; }
        public FormaPagamentoEnum Forma { get; private set; }
        public StatusPagamentoEnum Status { get; private set; }

        protected PagamentoEntity() { }

        public PagamentoEntity(int idPedido, FormaPagamentoEnum forma, StatusPagamentoEnum status)
        {
            Update(idPedido, forma, status);
        }

        public void Update(int idPedido, FormaPagamentoEnum forma, StatusPagamentoEnum status)
        {
            if (idPedido <= 0)
                throw new ArgumentException("Id do pedido inválido.");

            IdPedido = idPedido;
            Forma = forma;
            Status = status;
        }

        public void ConfirmarPagamento()
        {
            if (Status == StatusPagamentoEnum.Pago)
                throw new InvalidOperationException("Pagamento já foi confirmado.");

            Status = StatusPagamentoEnum.Pago;
        }

        public void CancelarPagamento()
        {
            if (Status == StatusPagamentoEnum.Cancelado)
                throw new InvalidOperationException("Pagamento já foi cancelado.");

            Status = StatusPagamentoEnum.Cancelado;
        }
    }
}
