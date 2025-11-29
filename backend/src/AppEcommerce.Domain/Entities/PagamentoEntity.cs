using System.Text.Json.Serialization;
using AppEcommerce.Domain.Enums;

namespace AppEcommerce.Domain.Entities;

public class PagamentoEntity
{
    [JsonInclude]
    public int Id { get; private set; }

    [JsonInclude]
    public int IdPedido { get; private set; }

    [JsonInclude]
    public FormaPagamentoEnum Forma { get; private set; }

    [JsonInclude]
    public StatusPagamentoEnum Status { get; private set; }

    // Construtor vazio para o serializador JSON
    public PagamentoEntity() { }

    // Construtor usado pela aplicação
    public PagamentoEntity(int idPedido, FormaPagamentoEnum forma, StatusPagamentoEnum status)
    {
        Update(idPedido, forma, status);
    }

    // Método necessário para o Repository injetar o ID gerado
    public void DefinirId(int id)
    {
        Id = id;
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