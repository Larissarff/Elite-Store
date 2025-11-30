using AppEcommerce.Application.DTOs.ItemCarrinho;
using AppEcommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemCarrinhoController : ControllerBase
    {
        private readonly IItemCarrinhoService _itemCarrinhoService;

        public ItemCarrinhoController(IItemCarrinhoService itemCarrinhoService)
        {
            _itemCarrinhoService = itemCarrinhoService;
        }

        // GET: api/ItemCarrinho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCarrinhoDto>>> GetAll()
        {
            var itens = await _itemCarrinhoService.GetAllAsync();
            return Ok(itens);
        }

        // GET: api/ItemCarrinho/carrinho/5
        [HttpGet("carrinho/{carrinhoId:int}")]
        public async Task<ActionResult<IEnumerable<ItemCarrinhoDto>>> GetByCarrinho(int carrinhoId)
        {
            var itens = await _itemCarrinhoService.GetByCarrinhoAsync(carrinhoId);
            return Ok(itens);
        }

        // GET: api/ItemCarrinho/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemCarrinhoDto>> GetById(int id)
        {
            var item = await _itemCarrinhoService.GetByIdAsync(id);

            if (item is null)
                return NotFound($"Item de carrinho {id} não encontrado.");

            return Ok(item);
        }

        // POST: api/ItemCarrinho
        [HttpPost]
        public async Task<ActionResult<ItemCarrinhoDto>> Post([FromBody] CreateItemCarrinhoDto dto)
        {
            try
            {
                var criado = await _itemCarrinhoService.AddAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = criado.ItemCarrinhoId },
                    criado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ItemCarrinho/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ItemCarrinhoDto>> Put(int id, [FromBody] UpdateItemCarrinhoDto dto)
        {
            try
            {
                var atualizado = await _itemCarrinhoService.UpdateAsync(id, dto);
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

        // DELETE: api/ItemCarrinho/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _itemCarrinhoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
