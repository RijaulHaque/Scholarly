using Scholarly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Scholarly.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // DbSet for Users table
        public DbSet<user> user { get; set; }

        // DbSet for Courses table
        public DbSet<Courses> course { get; set; }

        // DbSet for Students table
        public DbSet<Students> student { get; set; }

        // DbSet for Teachers table
        public DbSet<Teachers> Teacher { get; set; }

        // DbSet for Enrollments table
        public DbSet<Enrollments> Enrollments { get; set; }

        // DbSet for Attendance table
        public DbSet<Attendance> Attendance { get; set; }

        // OnModelCreating for additional configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Users table
            modelBuilder.Entity<user>(entity =>
            {
                entity.ToTable("user"); // Specify the exact table name
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Roles).IsRequired();
            });

            // Configure Courses table
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.HasMany(c => c.Enrollments)
                      .WithOne()
                      .HasForeignKey(e => e.Id);
            });

            // Configure Students table
            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(s => s.Name).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Year).IsRequired();
            });

            // Configure Teachers table
            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
            });

            // Configure Enrollments table
            modelBuilder.Entity<Enrollments>(entity =>
            {
                entity.Property(e => e.Grade).IsRequired();
                entity.HasMany(e => e.Attendance)
                      .WithOne()
                      .HasForeignKey(a => a.Id);
            });

            // Configure Attendance table
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(a => a.Date).IsRequired();
                entity.Property(a => a.AttendanceData).IsRequired();
            });

            // Course → Enrollment (No Cascade)
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Courses)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CoursesId) // Corrected foreign key property name
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Enrollment → Attendance (Cascade Delete)
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Enrollments)
                .WithMany(e => e.Attendance)
                .HasForeignKey(a => a.EnrollmentsId) // Corrected foreign key property name
                .OnDelete(DeleteBehavior.Cascade); // Automatically delete attendance when enrollment is deleted


            base.OnModelCreating(modelBuilder); // Ensure the base method is called
        }

        // To ignore the database migration change error
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
