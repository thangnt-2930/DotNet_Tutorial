using System.ComponentModel.DataAnnotations;

namespace Enrollments.Models;

public class Enrollment
{
    [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter the StudentId")]
    public int StudentId { get; set; }
    public Student.Models.Student Student { get; set; }

    [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter the CourseId")]
    public int CourseId { get; set; }
    public Course.Models.Course Course { get; set; }
}
