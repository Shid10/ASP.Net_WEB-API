using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Class2107.Models
{
    public class SQLAuthorRepository : IAuthorinterface
    {
        private readonly AppdbContext _context;
        public SQLAuthorRepository(AppdbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Author>> Add(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Delete(int Id)
        {
            Author author = _context.Authors.Find(Id);
            if(author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            return author;
        }

        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthor()
        {
            if(_context.Authors == null)
            {
                return null;
            }
            return await _context.Authors.ToListAsync();
        }

        public async Task<ActionResult<Author>?> GetAuthor(int Id)
        {
           if(_context.Authors == null)
            {
                return null;
            }
           var author = await _context.Authors.FindAsync(Id);
            if(author == null)
            {
                return null;
            }
            return author;
        }

        public ActionResult<IEnumerable<dynamic>> GetBooks(string name)
        {
            var books = from book in _context.Books
                        join author in _context.Authors on book.AuthorId equals author.AuthorId
                        where author.FirstName == name select book;
            return books.ToList();
        }
        

        public async Task<Author> Update(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return null;
            }
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                _context.Update(author);
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();

        }
    }
}
