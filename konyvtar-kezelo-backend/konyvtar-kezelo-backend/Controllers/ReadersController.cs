using Microsoft.AspNetCore.Mvc;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services.Interfaces;

namespace konyvtar_kezelo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadersController : ControllerBase
{
    private readonly IReaderService _readerService;

    public ReadersController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _readerService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reader = await _readerService.GetByIdAsync(id);
        return reader is null ? NotFound() : Ok(reader);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Reader reader)
    {
        try
        {
            var created = await _readerService.CreateAsync(reader);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Reader reader)
    {
        try
        {
            var updated = await _readerService.UpdateAsync(id, reader);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _readerService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}