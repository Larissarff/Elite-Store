namespace AppEcommerce.Application.DTOs.ItemCarrinho
{
    public class ItemCarrinhoDto
    {
        public int ItemCarrinhoId { get; set; }
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class CreateItemCarrinhoDto
    {
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }

    public class UpdateItemCarrinhoDto
    {
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
