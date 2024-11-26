using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportsManagementAPI.Data.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int LeagueId { get; set; }

        [ForeignKey(nameof(LeagueId))]
        public virtual League League { get; set; }
    }
}
