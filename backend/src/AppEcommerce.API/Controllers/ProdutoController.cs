using AppEcommerce.Application.Interfaces;
using AppEcommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoEntity>>> GetAll()
    {
        var produtos = await _service.GetAllAsync();
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoEntity>> GetById(int id)
    {
        var produto = await _service.GetByIdAsync(id);
        if (produto is null) return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProdutoEntity produto)
    {
        if (produto is null) return BadRequest("Produto inválido.");
        await _service.AddAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProdutoEntity produto)
    {
        if (produto is null || id != produto.Id)
            return BadRequest("ID do produto inválido.");

        await _service.UpdateAsync(produto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
