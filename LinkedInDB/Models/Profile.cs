namespace LinkedInDB.Models
{
        public class Profile
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Headline { get; set; } = string.Empty;
            public string Summary { get; set; } = string.Empty;
            public string Industry { get; set; } = string.Empty;
            public string Location { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
        }
}


