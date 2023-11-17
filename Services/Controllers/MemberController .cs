using LibraryApp.Repository;
using LibraryApp.Repository.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Services.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepo _repo;

        public MemberController(IMemberRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Member>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                List<Member> Members = await _repo.GetAll(cancellationToken);
                var sth = Members;
                if (Members.Count > 0)
                {
                    return Ok(Members);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Member))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemberById(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest();
                }

                Member Member = await _repo.GetById(id, cancellationToken);
                if (Member != null)
                {
                    return Ok(Member);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostMember([FromBody] Member Member, CancellationToken cancellationToken = new CancellationToken())
        {
            if (Member == null)
            {
                return BadRequest("Member data is null.");
            }

            await _repo.Add(Member, cancellationToken);
            return CreatedAtAction(nameof(GetMemberById), new { id = Member.Id }, Member);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutMember(string id, [FromBody] Member Member, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var existingMember = await _repo.GetById(id, cancellationToken);
                if (existingMember == null)
                {
                    return NotFound();
                }

                await _repo.Update(id, Member, cancellationToken);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var existingMember = await _repo.GetById(id, cancellationToken);
                if (existingMember == null)
                {
                    return NotFound();
                }

                await _repo.Delete(existingMember, cancellationToken);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
