using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly IPagamentoService _service;

    public PagamentoController(IPagamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PagamentoEntity>>> GetAll()
    {
        var pagamentos = await _service.GetAllAsync();
        return Ok(pagamentos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PagamentoEntity>> GetById(int id)
    {
        try
        {
            var pagamento = await _service.GetByIdAsync(id);
            if (pagamento is null) return NotFound();
            return Ok(pagamento);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PagamentoEntity pagamento)
    {
        if (pagamento is null) return BadRequest("Pagamento inválido.");

        await _service.AddAsync(pagamento);
        return CreatedAtAction(nameof(GetById), new { id = pagamento.Id }, pagamento);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] PagamentoEntity pagamento)
    {
        if (pagamento is null || id != pagamento.Id)
            return BadRequest("ID do pagamento inválido.");

        await _service.UpdateAsync(pagamento);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // Endpoint exemplo usando polimorfismo (taxa por forma de pagamento)
    [HttpGet("calcular-valor")]
    public async Task<ActionResult<decimal>> CalcularValor(
        [FromQuery] FormaPagamentoEnum forma,
        [FromQuery] decimal valorPedido)
    {
        try
        {
            var total = await _service.CalcularValorComTaxaAsync(forma, valorPedido);
            return Ok(total);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}