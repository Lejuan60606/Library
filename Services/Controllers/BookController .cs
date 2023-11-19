using Repository;
using Repository.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace Services.Controllers
{  
    [Route("api/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo _repo;

        public BookController(IBookRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("books")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Book>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken = new CancellationToken())
        {        
            try
            {
                List<Book> books = await _repo.GetAll(cancellationToken);
                if(books.Count > 0)
                {
                    return Ok(books);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if(string.IsNullOrEmpty(id))
                {
                    return BadRequest();
                }

                Book book = await _repo.GetById(id, cancellationToken);
                if(book != null)               
                {
                    return Ok(book);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostBook([FromBody] Book book, CancellationToken cancellationToken = new CancellationToken())
        {
            if (book == null)
            {
                return BadRequest("Book data is null.");
            }
             
            await _repo.Add(book, cancellationToken);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut]
        [Route("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutBook(string id, [FromBody] Book book, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var existingBook = await _repo.GetById(id, cancellationToken);
                if (existingBook == null)
                {
                    return NotFound();
                }

                await _repo.Update(id, book, cancellationToken);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("book/{id}")]
        public async Task<IActionResult> DeleteBook(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var existingBook = await _repo.GetById(id, cancellationToken);
                if (existingBook == null)
                {
                    return NotFound();
                }

                await _repo.Delete(existingBook, cancellationToken);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
