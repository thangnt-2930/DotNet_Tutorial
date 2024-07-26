using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Student.Models;
using Xunit;

namespace Student.Tests.Models
{
    public class StudentTests
    {
        [Fact]
        public void Student_NameIsRequired_ShouldFailIfNameIsEmpty()
        {
            var student = new Student.Models.Student
            {
                Id = 1,
                Name = "",
                Enrollments = new List<Enrollments.Models.Enrollment>()
            };

            var validationResults = ValidateModel(student);

            Assert.Single(validationResults);
            Assert.Equal("Please enter the name", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void Student_ValidModel_ShouldPassValidation()
        {
            var student = new Student.Models.Student
            {
                Id = 1,
                Name = "John Doe",
                Enrollments = new List<Enrollments.Models.Enrollment>()
            };

            var validationResults = ValidateModel(student);

            Assert.Empty(validationResults);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
