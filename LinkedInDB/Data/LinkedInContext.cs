using LinkedInDB.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkedInDB.Data
{
    public class LinkedInContext : DbContext
    {
        public LinkedInContext(DbContextOptions<LinkedInContext> options)
            : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Education> Educations { get; set; } // I hate that's it plural
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
