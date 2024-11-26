namespace SportsManagementAPI.Models
{
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? TeamId { get; set; }
    }
}
