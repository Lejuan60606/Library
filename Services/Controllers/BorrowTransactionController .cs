using Repository;
using Repository.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace Services.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BorrowTransactionController : ControllerBase
    {
        private readonly IBorrowTransactionRepo _repo;

        public BorrowTransactionController(IBorrowTransactionRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("transactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BorrowTransaction>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBorrowTransactions(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                List<BorrowTransaction> BorrowTransactions = await _repo.GetAll(cancellationToken);
                var sth = BorrowTransactions;
                if (BorrowTransactions.Count > 0)
                {
                    return Ok(BorrowTransactions);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("transaction/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BorrowTransaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBorrowTransactionById(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest();
                }

                BorrowTransaction BorrowTransaction = await _repo.GetById(id, cancellationToken);
                if (BorrowTransaction != null)
                {
                    return Ok(BorrowTransaction);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("transaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostBorrowTransaction([FromBody] BorrowTransaction borrowTransaction, CancellationToken cancellationToken = new CancellationToken())
        {
            if (borrowTransaction == null)
            {
                return BadRequest("BorrowTransaction data is null.");
            }

            await _repo.Add(borrowTransaction, cancellationToken);
            return CreatedAtAction(nameof(GetBorrowTransactionById), new { id = borrowTransaction.Id }, borrowTransaction);
        }

        [HttpPut]
        [Route("transaction/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutBorrowTransaction(string id, [FromBody] BorrowTransaction borrowTransaction, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var existingBorrowTransaction = await _repo.GetById(id, cancellationToken);
                if (existingBorrowTransaction == null)
                {
                    return NotFound();
                }

                await _repo.Update(id, borrowTransaction, cancellationToken);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("transaction/{id}")]
        public async Task<IActionResult> DeleteBorrowTransaction(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var existingBorrowTransaction = await _repo.GetById(id, cancellationToken);
                if (existingBorrowTransaction == null)
                {
                    return NotFound();
                }

                await _repo.Delete(existingBorrowTransaction, cancellationToken);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
