using System.ComponentModel.DataAnnotations;

namespace SportsManagementAPI.Models
{
    public class LeagueRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
