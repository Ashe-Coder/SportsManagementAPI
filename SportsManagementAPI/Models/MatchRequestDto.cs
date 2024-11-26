using System.ComponentModel.DataAnnotations;

namespace SportsManagementAPI.Models
{
    public class MatchRequestDto
    {
        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }
    }
}
