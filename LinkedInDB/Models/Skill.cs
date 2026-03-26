namespace LinkedInDB.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string SkillName { get; set; } = string.Empty;
    }
}
