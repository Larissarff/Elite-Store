using AppEcommerce.Application.Services;
using AppEcommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidoController(PedidoService service)
        {
            _service = service;
        }

        // GET: api/pedido
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        // GET: api/pedido/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);
            if (pedido is null)
                return NotFound("Pedido não encontrado.");

            return Ok(pedido);
        }

        // POST: api/pedido
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoEntity pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
        }

        // PUT: api/pedido/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PedidoEntity pedido)
        {
            if (id != pedido.IdPedido)
                return BadRequest("ID do pedido inválido.");

            await _service.UpdateAsync(pedido);
            return NoContent();
        }

        // DELETE: api/pedido/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}