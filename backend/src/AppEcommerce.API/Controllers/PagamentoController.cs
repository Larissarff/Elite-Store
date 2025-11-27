using AppEcommerce.Application.Services;
using AppEcommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly PagamentoService _service;

    public PagamentoController(PagamentoService service)
    {
        _service = service;
    }

    // GET: api/pagamento
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pagamentos = await _service.GetAllAsync();
        return Ok(pagamentos);
    }

    // GET: api/pagamento/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pagamento = await _service.GetByIdAsync(id);
        if (pagamento is null)
            return NotFound("Pagamento não encontrado.");

        return Ok(pagamento);
    }

    // POST: api/pagamento
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PagamentoEntity pagamento)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.AddAsync(pagamento);
        return CreatedAtAction(nameof(GetById), new { id = pagamento.Id }, pagamento);
    }

    // PUT: api/pagamento/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PagamentoEntity pagamento)
    {
        if (id != pagamento.Id)
            return BadRequest("ID do pagamento inválido.");

        await _service.UpdateAsync(pagamento);
        return NoContent();
    }

    // DELETE: api/pagamento/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
