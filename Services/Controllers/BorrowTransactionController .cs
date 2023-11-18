using Repository;
using Repository.DataModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Services.Controllers
{
    [ApiController]
    [Route("api/borrowtransactions")]
    public class BorrowTransactionController : ControllerBase
    {
        private readonly IBorrowTransactionRepo _transactionRepository;

        public BorrowTransactionController(IBorrowTransactionRepo transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Route("member/books/{memberId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BorrowTransaction>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByMemberId(string memberId, CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return BadRequest("Transaction data is null.");
            }

            var transactions = await _transactionRepository.GetByMemberId(memberId, cancellationToken);
            if (transactions == null || !transactions.Any())
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        [HttpPost]
        [Route("member/borrow/{memberId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BorrowTransaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrowBook(string memberId, [FromBody] Book book, CancellationToken cancellationToken = new CancellationToken())
        {
            if(string.IsNullOrEmpty(memberId))
            {
                return BadRequest("Transaction data is null.");
            }

            Guid newId = Guid.NewGuid();
            string uniqueIdString = newId.ToString();
            BorrowTransaction transaction = new BorrowTransaction() { 
                Id = uniqueIdString,
                BookID = book.Id,
                MemberID = memberId,
                BorrowDate = DateTime.UtcNow
            };
            await _transactionRepository.BorrowBook(memberId, book, cancellationToken); //update book IsAvaliable status
            return CreatedAtAction(nameof(GetByMemberId), new { memberId = transaction.MemberID }, transaction);
        }

        [HttpPut]
        [Route("member/return/{memberId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BorrowTransaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReturnBook(string memberId, [FromBody] Book book, CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return BadRequest("member Id is null.");
            }

            if(book == null)
            {
                return BadRequest("Book is null");
            }

            //get the transaction ID, update the ReturnDate,update book Isavaliavle
           var transaction = await _transactionRepository.ReturnBook(memberId, book, cancellationToken);
            return Ok(transaction);
        }
    }


}
