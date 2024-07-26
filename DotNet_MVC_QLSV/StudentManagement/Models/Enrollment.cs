using System.ComponentModel.DataAnnotations;

namespace Enrollments.Models;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student.Models.Student Student { get; set; }

    public int CourseId { get; set; }
    public Course.Models.Course Course { get; set; }
}
