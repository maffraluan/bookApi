using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly BookService _bookService;

    public BooksController(BookService service)
    {
      _bookService = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
      var books = await _bookService.GetAllAsync();
      return Ok(books);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> GetById(string id)
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
      if (book == null)
      {
        return BadRequest();
      }
      
      await _bookService.CreateAsync(book);
      return Ok(book);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
      var book = await _bookService.GetByIdAsync(id);
      if (book == null)
      {
        return NotFound();
      }
      await _bookService.UpdateAsync(id, updatedBook);
      return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
      var book = await _bookService.GetByIdAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      await _bookService.DeleteAsync(book.Id);
      return NoContent();
    }
  }
}