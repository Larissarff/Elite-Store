namespace AppEcommerce.Application.DTOs.Carrinho
{
    // Item que vai aparecer dentro do carrinho na resposta da API
    public class ItemCarrinhoResumoDto
    {
        public int IDProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
    }

    // DTO de saída do carrinho
    public class CarrinhoDto
    {
        public int IdCarrinho { get; set; }
        public int IdCliente { get; set; }
        public List<ItemCarrinhoResumoDto> Itens { get; set; } = new();
        public decimal Total { get; set; }
    }

    // DTO para criar um carrinho
    public class CreateCarrinhoDto
    {
        public int IdCarrinho { get; set; }   // você que define o ID ao criar
        public int IdCliente { get; set; }
    }

    // DTO para adicionar item no carrinho
    public class AddItemCarrinhoDto
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
