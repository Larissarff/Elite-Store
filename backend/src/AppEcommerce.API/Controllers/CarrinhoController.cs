using AppEcommerce.Application.DTOs.Carrinho;
using AppEcommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        // GET: api/Carrinho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoDto>>> GetAll()
        {
            var carrinhos = await _carrinhoService.GetAllAsync();
            return Ok(carrinhos);
        }

        // GET: api/Carrinho/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarrinhoDto>> GetById(int id)
        {
            var carrinho = await _carrinhoService.GetByIdAsync(id);

            if (carrinho is null)
                return NotFound($"Carrinho {id} não encontrado.");

            return Ok(carrinho);
        }

        // POST: api/Carrinho
        [HttpPost]
        public async Task<ActionResult<CarrinhoDto>> Create([FromBody] CreateCarrinhoDto dto)
        {
            try
            {
                var criado = await _carrinhoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = criado.IdCarrinho }, criado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // POST: api/Carrinho/5/itens
        [HttpPost("{id:int}/itens")]
        public async Task<ActionResult<CarrinhoDto>> AddItem(int id, [FromBody] AddItemCarrinhoDto dto)
        {
            try
            {
                var atualizado = await _carrinhoService.AddItemAsync(id, dto);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Carrinho/5/itens/10
        [HttpDelete("{id:int}/itens/{idProduto:int}")]
        public async Task<ActionResult<CarrinhoDto>> RemoveItem(int id, int idProduto)
        {
            try
            {
                var atualizado = await _carrinhoService.RemoveItemAsync(id, idProduto);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Carrinho/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _carrinhoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
