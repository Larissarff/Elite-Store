using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoEntity>>> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoEntity>> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);
            if (pedido is null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidoEntity pedido)
        {
            if (pedido is null) return BadRequest("Pedido inválido.");

            await _service.AddAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PedidoEntity pedido)
        {
            if (pedido is null || id != pedido.IdPedido)
                return BadRequest("ID do pedido inválido.");

            await _service.UpdateAsync(pedido);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}