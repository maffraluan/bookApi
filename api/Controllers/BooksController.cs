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
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      
      await _bookService.CreateAsync(book);
      return Ok(book);
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, Book updateBook)
    {
      var queriedBook = await _bookService.GetByIdAsync(id);
      if (queriedBook == null)
      {
        return NotFound();
      }
      await _bookService.UpdateAsync(id, updateBook);
      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
      var book = await _bookService.GetByIdAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      await _bookService.DeleteAsync(id);
      return NoContent();
    }
  }
}