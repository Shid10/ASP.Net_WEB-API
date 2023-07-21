using Class2107.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Class2107.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorinterface repository;

        public AuthorController(IAuthorinterface repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (await repository.GetAllAuthor() == null)
            {
                return NotFound();
            }

            return await repository.GetAllAuthor();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            var author = await repository.GetAuthor(id);
            return author == null ? NotFound() : author;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }
            try
            {
                await repository.Update(id, author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (repository.GetAuthor(id) == null)
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
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            await repository.Add(author);
            return CreatedAtAction("PostAuthor", new { id = author.AuthorId }, author);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (repository.GetAllAuthor() == null)
            {
                return NotFound();
            }

            await repository.Delete(id);
            return NoContent();
        }
        [HttpGet("{name}")]
        public ActionResult<IEnumerable<dynamic>> GetBooks(string name)
        {
            var book = repository.GetBooks(name);
            return book == null ? NotFound() : book;
        }
    }
}
