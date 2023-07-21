using Class2107.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Class2107.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherinterface _repository;
        public PublisherController(IPublisherinterface repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> GetAllPublisher()
        {
           if(_repository.GetAllPublisher() == null)
            {
                return NotFound();
            };
            return _repository.GetAllPublisher();
        }
        [HttpGet("{id}")]
        public ActionResult<Publisher> GetById(int id)
        {
            var publisher = _repository.GetPublisher(id);
            return publisher == null ? NotFound() : publisher;
        }
        [HttpPost]
        public ActionResult<Publisher> Post(Publisher publisher)
        {
            _repository.Add(publisher);
            return publisher;
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (_repository.GetAllPublisher() == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.PulisherId)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(id, publisher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_repository.GetPublisher(id)==null)
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
    }
}
