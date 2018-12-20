using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPIGenericSample.Context;
using NetCoreAPIGenericSample.Models;
using NetCoreAPIGenericSample.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreAPIGenericSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BaseService<Books> service;

        public BooksController(DataContext context)
        {
            service = new BaseService<Books>(context);
        }

        // GET: api/Books
        [HttpGet]
        public IEnumerable<Books> GetBooks()
        {
            return service.GetAll();
        }

        // GET: api/Books/Async
        [HttpGet("Async")]
        public async Task<IEnumerable<Books>> GetBooksAsync()
        {
            return await service.GetAllAsync();

        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult GetBooks([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Books books = service.Get(id);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // GET: api/Books/Async/5
        [HttpGet("Async/{id}")]
        public async Task<IActionResult> GetBooksAsync([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Books books = await service.GetAsync(id);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public IActionResult PutBooks([FromRoute] long id, [FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != books.Id)
            {
                return BadRequest();
            }
            try
            {
                service.Update(books, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
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

        // PUT: api/Books/Async/5
        [HttpPut("Async/{id}")]
        public Task<Books> PutBooksAsync([FromRoute] long id, [FromBody] Books books)
        {
            return service.UpdateAsync(books, id);
        }

        // POST: api/Books
        [HttpPost]
        public IActionResult PostBooks([FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.Add(books);

            return CreatedAtAction("GetBooks", new { id = books.Id }, books);
        }

        // POST: api/Books/Async
        [HttpPost("Async")]
        public async Task<IActionResult> PostBooksAsync([FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.AddAsync(books);

            return CreatedAtAction("GetBooks", new { id = books.Id }, books);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Books books = await service.GetAsync(id);
            if (books == null)
            {
                return NotFound();
            }

            service.Delete(books);

            return Ok(books);
        }


        private bool BooksExists(long id)
        {
            return service.Any(e => e.Id == id);
        }
    }
}