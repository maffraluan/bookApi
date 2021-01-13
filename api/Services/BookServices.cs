using BookApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
  public class BookService
  {
    private readonly IMongoCollection<Book> _books;

    public BookService(IBookstoreDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      _books = database.GetCollection<Book>(settings.BooksCollectionName);
    }

    public async Task<List<Book>> GetAllAsync(){
      return await _books.Find(b => true).ToListAsync();
    }

    public async Task<Book> GetByIdAsync(string id)
    {
      return await _books.Find<Book>(b => b.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Book> CreateAsync(Book book)
    {
      await _books.InsertOneAsync(book);
      return book;
    }

    public async Task UpdateAsync(string id, Book book)
    {
      await _books.ReplaceOneAsync(b => b.Id == id, book);
    }

    public async Task DeleteAsync(string id)
    {
      await _books.DeleteOneAsync(b => b.Id == id);
    }
  }
}