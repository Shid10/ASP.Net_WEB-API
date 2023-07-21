using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Class2107.Models
{
    public class SQLPublisherRepository : IPublisherinterface
    {
        private readonly AppdbContext _context;
        public SQLPublisherRepository(AppdbContext context)
        {
            _context = context;
        }
        public ActionResult<Publisher> Add(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }

        public Publisher Delete(int id)
        {
            Publisher publisher = _context.Publishers.Find(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            return publisher;
        }

        public ActionResult<IEnumerable<Publisher>> GetAllPublisher()
        {
            if (_context.Publishers == null)
            {
                return null;
            }
            return _context.Publishers.ToList();
        }

        public ActionResult<Publisher>? GetPublisher(int Id)
        {
            if (_context.Publishers == null)
            {
                return null;
            }
            var publisher = _context.Publishers.Find(Id);
            if (publisher == null)
            {
                return null;
            }
            return publisher;
        }

        public ActionResult<Publisher> Update(int id, Publisher publisher)
        {
            if (id != publisher.PulisherId)
            {
                return null;
            }
            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                _context.Update(publisher);
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        private bool PublisherExists(int id)
        {
            return (_context.Publishers?.Any(e => e.PulisherId == id)).GetValueOrDefault();

        }
    }
}
   

