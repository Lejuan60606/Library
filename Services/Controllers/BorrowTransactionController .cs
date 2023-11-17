using LibraryApp.Repository.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowTransactionController : ControllerBase
    {       
        [HttpGet("ByMember/{memberId}")]
        public IActionResult GetBorrowTransactionsByMember(int memberId)
        {          
            return Ok();
        }
      
        [HttpPost]
        public IActionResult PostBorrowTransaction([FromBody] BorrowTransaction transaction)
        {          
            return CreatedAtAction("GetBorrowTransactionsByMember", new { memberId = transaction.MemberID }, transaction);
        }
      
        [HttpPut("{id}")]
        public IActionResult PutBorrowTransaction(int id, [FromBody] BorrowTransaction transaction)
        {            
            return NoContent();
        }
       
    }
}

