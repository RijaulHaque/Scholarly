////////########################### 24th jun added######################
//using Scholarly.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;

//namespace Scholarly.DAL
//{
//    public class DatabaseContext : DbContext
//    {
//        public DatabaseContext(DbContextOptions<DatabaseContext> options)
//            : base(options)
//        {
//        }

//        // DbSet for Users table
//        public DbSet<user> user { get; set; }

//        // DbSet for Courses table
//        public DbSet<Courses> course { get; set; }

//        // DbSet for Students table
//        public DbSet<Students> student { get; set; }

//        // DbSet for Teachers table
//        public DbSet<Teachers> Teacher { get; set; }

//        // DbSet for Enrollments table
//        public DbSet<Enrollments> Enrollments { get; set; }

//        // DbSet for Attendance table
//        public DbSet<Attendance> Attendance { get; set; }

//        // OnModelCreating for additional configurations
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Configure Users table
//            modelBuilder.Entity<user>(entity =>
//            {
//                entity.ToTable("user"); // Specify the exact table name
//                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
//                entity.Property(u => u.Email).IsRequired();
//                entity.Property(u => u.PasswordHash).IsRequired();
//                entity.Property(u => u.Roles).IsRequired();
//            });

//            // Configure Courses table
//            modelBuilder.Entity<Courses>(entity =>
//            {
//                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
//                entity.Property(c => c.Semester);
//                entity.Property(c => c.TeachersId);
//                entity.HasMany(c => c.Enrollments)
//                      .WithOne(e => e.Courses)
//                      .HasForeignKey(e => e.CoursesId); // Corrected foreign key property name
//            });

//            // Configure Students table
//            modelBuilder.Entity<Students>(entity =>
//            {
//                entity.Property(s => s.Name).IsRequired().HasMaxLength(50);
//                entity.Property(s => s.Year);
//                entity.Property(s => s.Email);
//                entity.Property(s => s.RegistrationNo).HasMaxLength(50);
//                entity.Property(s => s.FullName).HasMaxLength(100);
//                entity.Property(s => s.CurrentSemester);
//                entity.Property(s => s.PhoneNo);
//                entity.Property(s => s.Address).HasMaxLength(200);

//                entity.HasMany(s => s.Enrollments)
//                      .WithOne()
//                      .HasForeignKey(e => e.StudentId); // Ensure foreign key is correctly set

//                // Configure foreign key to user
//                entity.HasOne(s => s.User)
//                      .WithMany()
//                      .HasForeignKey(s => s.UserId);
//            });

//            // Configure Teachers table
//            modelBuilder.Entity<Teachers>(entity =>
//            {
//                entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
//                entity.Property(t => t.EmployeeRegistrationNo).HasMaxLength(50);
//                entity.Property(t => t.FullName).HasMaxLength(100);
//                entity.Property(t => t.PhoneNo); // Assuming PhoneNo should be a string with a max length
//                entity.Property(t => t.Address).HasMaxLength(200);

//                entity.HasMany(t => t.Courses)
//                      .WithOne()
//                      .HasForeignKey(c => c.TeachersId); // Ensure foreign key is correctly set

//                // Configure foreign key to user
//                entity.HasOne(t => t.User)
//                      .WithMany()
//                      .HasForeignKey(t => t.UserId);
//            });

//            // Configure Enrollments table
//            modelBuilder.Entity<Enrollments>(entity =>
//            {
//                entity.Property(e => e.Grade).HasMaxLength(100);
//                entity.Property(e => e.StudentId);
//                entity.Property(e => e.TeachersId);
//                entity.Property(e => e.CoursesId);
//                entity.Property(e => e.Year);
//                entity.HasMany(e => e.Attendance)
//                      .WithOne(a => a.Enrollments)
//                      .HasForeignKey(a => a.EnrollmentsId); // Corrected foreign key property name
//            });

//            // Configure Attendance table
//            modelBuilder.Entity<Attendance>(entity =>
//            {
//                entity.Property(a => a.Date).IsRequired();
//                entity.Property(a => a.AttendanceData).IsRequired();
//            });

//            // Course → Enrollment (No Cascade)
//            modelBuilder.Entity<Enrollments>()
//                .HasOne(e => e.Courses)
//                .WithMany(c => c.Enrollments)
//                .HasForeignKey(e => e.CoursesId) // Corrected foreign key property name
//                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

//            // Enrollment → Attendance (Cascade Delete)
//            modelBuilder.Entity<Attendance>()
//                .HasOne(a => a.Enrollments)
//                .WithMany(e => e.Attendance)
//                .HasForeignKey(a => a.EnrollmentsId) // Corrected foreign key property name
//                .OnDelete(DeleteBehavior.Cascade); // Automatically delete attendance when enrollment is deleted

//            base.OnModelCreating(modelBuilder); // Ensure the base method is called
//        }

//        // To ignore the database migration change error
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder
//                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
//        }
//    }
//}

//25th jan
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
                entity.Property(c => c.Semester);
                entity.Property(c => c.TeachersId);
                entity.HasMany(c => c.Enrollments)
                      .WithOne(e => e.Courses)
                      .HasForeignKey(e => e.CoursesId); // Corrected foreign key property name
            });

            // Configure Students table
            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(s => s.Name).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Year);
                entity.Property(s => s.Email);
                entity.Property(s => s.RegistrationNo).HasMaxLength(50);
                entity.Property(s => s.FullName).HasMaxLength(100);
                entity.Property(s => s.CurrentSemester);
                entity.Property(s => s.PhoneNo);
                entity.Property(s => s.Address).HasMaxLength(200);

                entity.HasMany(s => s.Enrollments)
                      .WithOne()
                      .HasForeignKey(e => e.StudentId); // Ensure foreign key is correctly set

                // Configure foreign key to user
                entity.HasOne(s => s.User)
                      .WithMany()
                      .HasForeignKey(s => s.UserId);
            });

            // Configure Teachers table
            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
                entity.Property(t => t.EmployeeRegistrationNo).HasMaxLength(50);
                entity.Property(t => t.FullName).HasMaxLength(100);
                entity.Property(t => t.PhoneNo); // Assuming PhoneNo should be a string with a max length
                entity.Property(t => t.Address).HasMaxLength(200);

                entity.HasMany(t => t.Courses)
                      .WithOne()
                      .HasForeignKey(c => c.TeachersId); // Ensure foreign key is correctly set

                // Configure foreign key to user
                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.UserId);
            });

            // Configure Enrollments table
            modelBuilder.Entity<Enrollments>(entity =>
            {
                entity.Property(e => e.Grade).HasMaxLength(100);
                entity.Property(e => e.StudentId);
                entity.Property(e => e.TeachersId);
                entity.Property(e => e.CoursesId);
                entity.Property(e => e.Year);
                entity.HasMany(e => e.Attendance)
                      .WithOne(a => a.Enrollments)
                      .HasForeignKey(a => a.EnrollmentsId); // Corrected foreign key property name
            });

            // Configure Attendance table
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(a => a.Date).IsRequired();
                entity.Property(a => a.AttendanceData).IsRequired();
                entity.Property(a => a.IsApproved).IsRequired();
                entity.Property(a => a.IsSubmitted).IsRequired();
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



