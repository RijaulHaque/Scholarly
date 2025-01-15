//using Scholarly.Models;
//using Microsoft.EntityFrameworkCore;



//namespace Scholarly.DAL
//{
//    public class DatabaseContext : DbContext
//    {
//        public DatabaseContext(DbContextOptions<DatabaseContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Users> User { get; set; }
//        public DbSet<Courses> course { get; set; }
//        public DbSet<Students> student { get; set; }
//        public DbSet<Teachers> Teacher{ get; set; }

//    }
//}

//#################### NEW CODE by Rijaul ###########################

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

        // DbSet for Users table
        public DbSet<Users> Users { get; set; }

        // DbSet for Courses table
        public DbSet<Courses> course { get; set; }

        // DbSet for Students table
        public DbSet<Students> student { get; set; }

        // DbSet for Teachers table
        public DbSet<Teachers> Teacher { get; set; }

        // OnModelCreating for additional configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Users table #### OLD CODE ####
            //modelBuilder.Entity<Users>(entity =>
            //{
            //    entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            //    entity.Property(u => u.Email).IsRequired();
            //    entity.Property(u => u.PasswordHash).IsRequired();
            //    entity.Property(u => u.Roles).IsRequired();
            //});
            // Configure Users table #### NEW CODE ####
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users"); // Specify the exact table name
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Roles).IsRequired();
            });



            // Additional configurations for other tables can be added similarly.
            //modelBuilder.Entity<Courses>(entity =>
            //{
            //    // Example configurations for Courses
            //    entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            //    entity.Property(c => c.Credits).IsRequired();
            //});

            //modelBuilder.Entity<Students>(entity =>
            //{
            //    // Example configurations for Students
            //    entity.Property(s => s.Name).IsRequired().HasMaxLength(50);
            //    entity.Property(s => s.Year).IsRequired();
            //});

            //modelBuilder.Entity<Teachers>(entity =>
            //{
            //    // Example configurations for Teachers
            //    entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
            //});

            base.OnModelCreating(modelBuilder); // Ensure the base method is called
        }
    }
}

