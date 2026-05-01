using Microsoft.AspNetCore.Mvc;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services.Interfaces;

namespace konyvtar_kezelo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _bookService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        var created = await _bookService.CreateAsync(book);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book book)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _bookService.UpdateAsync(id, book);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var deleted = await _bookService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}