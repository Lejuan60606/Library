using LibraryApp.Repository.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Services.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {       
        [HttpGet]
        public IActionResult GetMembers()
        {        
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostMember([FromBody] Member Member)
        {
            return CreatedAtAction(nameof(GetMemberById), new { id = Member.Id }, Member);
        }

        [HttpPut("{id}")]
        public IActionResult PutMember(int id, [FromBody] Member Member)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            return Ok();
        }
    }

}
