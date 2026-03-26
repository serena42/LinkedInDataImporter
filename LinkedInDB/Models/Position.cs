namespace LinkedInDB.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
    }
}
