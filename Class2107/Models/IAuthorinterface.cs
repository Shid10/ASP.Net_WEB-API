using Microsoft.AspNetCore.Mvc;

namespace Class2107.Models
{
    public interface IAuthorinterface
    {
        Task<ActionResult<Author>?> GetAuthor(int Id);
        Task<ActionResult<IEnumerable<Author>>> GetAllAuthor();
        Task<ActionResult<Author>> Add(Author author);
        Task<Author> Update(int id, Author author);
        Task<Author> Delete(int Id);
        ActionResult<IEnumerable<dynamic>> GetBooks(string name);
    }
}
