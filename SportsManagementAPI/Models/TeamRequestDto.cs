using System.ComponentModel.DataAnnotations;

namespace SportsManagementAPI.Models
{
    public class TeamRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int LeagueId { get; set; }
    }
}
