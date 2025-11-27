using AppEcommerce.Application.Services;
using AppEcommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppEcommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutoController(ProdutoService service)
    {
        _service = service;
    }

    // 🔹 GET: api/produto
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var produtos = await _service.GetAllAsync();
        return Ok(produtos);
    }

    // 🔹 GET: api/produto/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _service.GetByIdAsync(id);
        if (produto is null)
            return NotFound("Produto não encontrado.");

        return Ok(produto);
    }

    // 🔹 POST: api/produto
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProdutoEntity produto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.AddAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    // 🔹 PUT: api/produto/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProdutoEntity produto)
    {
        if (id != produto.Id)
            return BadRequest("ID do produto inválido.");

        await _service.UpdateAsync(produto);
        return NoContent();
    }

    // 🔹 DELETE: api/produto/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
