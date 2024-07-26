using System.ComponentModel.DataAnnotations;

namespace Student.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Enrollments.Models.Enrollment>? Enrollments { get; set; }
}
