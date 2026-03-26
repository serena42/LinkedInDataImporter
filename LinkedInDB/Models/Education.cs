namespace LinkedInDB.Models
{
    public class Education
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string SchoolName { get; set; } = string.Empty;
        public string DegreeName { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
    }
}
