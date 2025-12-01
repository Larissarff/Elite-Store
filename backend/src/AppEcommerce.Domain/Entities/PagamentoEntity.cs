using AppEcommerce.Domain.Enums;

namespace AppEcommerce.Domain.Entities
{
    /// <summary>
    /// Classe base abstrata para representar regras variáveis
    /// de forma de pagamento (taxas, comportamento, etc.).
    /// </summary>
    public abstract class PagamentoEntity
    {
        public abstract FormaPagamentoEnum Forma { get; }

        public virtual decimal CalcularTaxaServico(decimal valorPedido)
        {
            return 0m;
        }
    }

    public class PagamentoPix : PagamentoEntity
    {
        public override FormaPagamentoEnum Forma => FormaPagamentoEnum.Pix;

        public override decimal CalcularTaxaServico(decimal valorPedido)
        {
            // Exemplo: Pix sem taxa
            return 0m;
        }
    }

    public class PagamentoCartaoCredito : PagamentoEntity
    {
        public override FormaPagamentoEnum Forma => FormaPagamentoEnum.CartaoCredito;

        public override decimal CalcularTaxaServico(decimal valorPedido)
        {
            // Exemplo: 2% de taxa no cartão de crédito
            return valorPedido * 0.02m;
        }
    }
}
