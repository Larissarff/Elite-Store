using AppEcommerce.Application.DTOs;
using AppEcommerce.Application.Requests;
using AppEcommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Post([FromBody] CreateClienteRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateClienteRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != request.Id) return BadRequest("Id do corpo difere do parâmetro.");

        await _service.UpdateAsync(request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}