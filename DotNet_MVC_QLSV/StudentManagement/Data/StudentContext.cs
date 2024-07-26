using Microsoft.EntityFrameworkCore;
using Student.Models;
using Course.Models;
using Enrollments.Models;

namespace Student.Data
{
    public class StudentContext : DbContext
    {
        public DbSet<Models.Student> Students { get; set; }
        public DbSet<Course.Models.Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }
}

