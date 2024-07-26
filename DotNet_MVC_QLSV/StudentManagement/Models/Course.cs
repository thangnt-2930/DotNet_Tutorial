using System.ComponentModel.DataAnnotations;

namespace Course.Models;

public class Course
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter the name")]
    public string Name { get; set; }
    
    public ICollection<Enrollments.Models.Enrollment> Enrollments { get; set; }
}
