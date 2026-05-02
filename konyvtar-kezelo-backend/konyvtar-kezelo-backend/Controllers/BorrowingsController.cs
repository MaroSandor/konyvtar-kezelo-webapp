using Microsoft.AspNetCore.Mvc;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services.Interfaces;

namespace konyvtar_kezelo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowingsController: ControllerBase
{
    private readonly IBorrowingService _borrowingService;

    public BorrowingsController(IBorrowingService borrowingService)
    {
        _borrowingService = borrowingService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _borrowingService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var borrowing = await _borrowingService.GetByIdAsync(id);
        return borrowing is null ? NotFound() : Ok(borrowing);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Borrowing borrowing)
    {
        try
        {
            var created = await _borrowingService.CreateAsync(borrowing);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Borrowing borrowing)
    {
        try
        {
            var updated = await _borrowingService.UpdateAsync(id, borrowing);
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
        var deleted = await _borrowingService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}