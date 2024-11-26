using Microsoft.AspNetCore.Mvc;
using SportsManagementAPI.Models;
using SportsManagementAPI.Services;

namespace SportsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeaguesController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeague([FromRoute] int id)
        {
            try
            {
                var leagueDto = await _leagueService.GetLeagueByIdAsync(id);
                return Ok(leagueDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLeague([FromBody] LeagueRequestDto leagueDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var league = await _leagueService.AddLeagueAsync(leagueDto);
                return Ok(league);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeague([FromRoute] int id, [FromBody] LeagueRequestDto leagueDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (leagueDto == null) return BadRequest("League data is required");

                await _leagueService.UpdateLeagueAsync(id, leagueDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            try
            {
                await _leagueService.DeleteLeagueAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
