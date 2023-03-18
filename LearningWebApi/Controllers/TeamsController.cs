using LearningWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private static List<Team> _teams = new List<Team>
        {
            new Team { Id = 1, Name = "Team A", Address = "123 Main St", Contact = "John Doe" },
            new Team { Id = 2, Name = "Team B", Address = "456 Broad St", Contact = "Jane Smith" },
            new Team { Id = 3, Name = "Team C", Address = "789 Park Ave", Contact = "Bob Johnson" }
        };

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            return Ok(_teams);
        }
        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            var team = _teams.FirstOrDefault(x => x.Id == id);
            if (team == null)
            {
                return NotFound("Team not found");
            }
            return Ok(team);
        }
        [HttpPost]
        public IActionResult AddTeam(Team team)
        {
            team.Id = _teams.Max(x => x.Id) + 1;
            _teams.Add(team);
            return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateTeam(int id, string name)
        {
            var team = _teams.FirstOrDefault(x => x.Id == id);
            if (team == null)
                return NotFound("Team not found");
            team.Name = name;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var team = _teams.FirstOrDefault(_ => _.Id == id);
            if (team == null)
                return NotFound("Team not found");
            _teams.Remove(team);
            return NoContent();
        }
    }
}
