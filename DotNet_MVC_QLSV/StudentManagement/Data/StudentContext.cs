using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    public class StudentContext : DbContext
    {
        public DbSet<Student.Models.Student> Students { get; set; }

        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }
    }
}

