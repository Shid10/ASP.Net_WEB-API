using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Class2107.Models
{
    public class SQLBookRepository : IBookinterface
    {
        private readonly AppdbContext context;

        public SQLBookRepository(AppdbContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult<Book>> Add(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Delete(int Id)
        {
            Book book = context.Books.Find(Id);
            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
            return book;
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetAllBook()
        {
            if (context.Books == null)
            {
                return null;
            }
            return await context.Books.ToListAsync();
        }

        public async Task<ActionResult<Book>?> GetBook(int Id)
        {

            if (context.Books == null)
            {
                return null;
            }
            var book = await context.Books.FindAsync(Id);
            if (book == null)
            {
                return null;
            }
            return book;
        }

        public ActionResult<IEnumerable<dynamic>> GetName(string name)
        {
            var q = context.Books.Join(context.Authors, authid => authid.AuthorId, bkid => bkid.AuthorId, (bkid, authid) => new { Title = bkid.Title, Firstname = authid.FirstName }).Where(bk => bk.Title == name);

            return q.ToList();

        }

        public async Task<Book> Update(int id, Book book)
        {
            if (id != book.BookId)
            {
                return null;
            }
            context.Entry(book).State = EntityState.Modified;

            try
            {
                context.Update(book);
                 context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        private bool BookExists(int id)
        {
            return (context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();

        }
    }
}
   
    