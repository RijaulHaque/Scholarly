using Scholarly.Models;
using Microsoft.EntityFrameworkCore;



namespace Scholarly.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Users> user { get; set; }
        public DbSet<Courses> course { get; set; }
        public DbSet<Students> student { get; set; }
        public DbSet<Teachers> Teacher{ get; set; }

    }
}
