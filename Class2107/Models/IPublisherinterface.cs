using Microsoft.AspNetCore.Mvc;

namespace Class2107.Models
{
    public interface IPublisherinterface
    {
        ActionResult<Publisher>? GetPublisher(int Id);
        ActionResult<IEnumerable<Publisher>> GetAllPublisher();
        ActionResult<Publisher> Add(Publisher publisher);
        ActionResult<Publisher> Update(int id, Publisher publisher);
        Publisher Delete(int id);
    }
}
