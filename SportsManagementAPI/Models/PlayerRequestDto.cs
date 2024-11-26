using System.ComponentModel.DataAnnotations;

namespace SportsManagementAPI.Models
{
    public class PlayerRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }
        public int? TeamId { get; set; }
    }
}
