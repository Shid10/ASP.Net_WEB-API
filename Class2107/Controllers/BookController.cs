using Class2107.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Class2107.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookinterface _repository;
        public BookController(IBookinterface repository)
        {
            _repository = repository;

        }
        // GET: api/<BooksController>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (await _repository.GetAllBook() == null)
            {
                return NotFound();
            }

            return await _repository.GetAllBook();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _repository.GetBook(id);
            return book == null ? NotFound() : book;
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<dynamic>> GetName(string name)
        {
            var book = _repository.GetName(name);
            return book == null ? NotFound() : book;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }
            try
            {
                await _repository.Update(id, book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_repository.GetBook(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            await _repository.Add(book);
            return CreatedAtAction("PostBook", new { id = book.BookId }, book);
        }
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_repository.GetAllBook() == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);
            return NoContent();
        }

    }
}
