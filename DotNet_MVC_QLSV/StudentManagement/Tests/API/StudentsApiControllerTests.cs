using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Xunit;

namespace StudentApi.Tests;
public class StudentsApiControllerTests
{
    private readonly StudentsApiController _controller;
    private readonly DbContextOptions<StudentContext> _contextOptions;

    public StudentsApiControllerTests()
    {
        _contextOptions = new DbContextOptionsBuilder<StudentContext>()
            .UseInMemoryDatabase(databaseName: "StudentApiDatabase")
            .Options;

        var context = new StudentContext(_contextOptions);
        _controller = new StudentsApiController(context);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [Fact]
    public async Task Index()
    {
        using (var context = new StudentContext(_contextOptions))
        {
            context.Students.Add(new Student.Models.Student { Id = 1, Name = "Ex 1" });
            context.Students.Add(new Student.Models.Student { Id = 2, Name = "Ex 2" });
            context.SaveChanges();
        }

        var result = await _controller.Index();

        var actionResult = Assert.IsType<ActionResult<IEnumerable<Student.Models.Student>>>(result);
        var students = Assert.IsType<List<Student.Models.Student>>(actionResult.Value);
        Assert.Equal(2, students.Count);
    }

    [Fact]
    public async Task Create()
    {
        var std = new Student.Models.Student { Id = 6, Name = "John Doe" };
        await _controller.CreateStudent(std);

        var student = await _controller.GetStudent(6);
        Assert.Equal("John Doe", student.Value.Name);

        var context = new StudentContext(_contextOptions);
        var stdCount = await context.Students.CountAsync();
        Assert.Equal(1, stdCount);
    }

    [Fact]
    public async Task GetStudent()
    {
        using (var context = new StudentContext(_contextOptions))
        {
            var std = new Student.Models.Student { Id = 3, Name = "John Doe" };
            context.Students.Add(std);
            context.SaveChanges();
        }

        var result = await _controller.GetStudent(3);

        var actionResult = Assert.IsType<ActionResult<Student.Models.Student>>(result);
        var student = Assert.IsType<Student.Models.Student>(actionResult.Value);
        Assert.Equal("John Doe", student.Name);
    }

    [Fact]
    public async Task Update()
    {
        using (var context = new StudentContext(_contextOptions))
        {
            var std = new Student.Models.Student { Id = 4, Name = "John Doe 4" };
            context.Students.Add(std);
            context.SaveChanges();
        }

        var updatedStudent = new Student.Models.Student { Id = 4, Name = "Name Update" };
        await _controller.Edit(4, updatedStudent);

        var student = await _controller.GetStudent(4);
        Assert.Equal("Name Update", student.Value.Name);
    }

    [Fact]
     public async Task UpdateFailed()
    {
        var updatedStudent = new Student.Models.Student { Id = 1, Name = "Update Test" };

        var result = await _controller.Edit(0, updatedStudent);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete()
    {
        using (var context = new StudentContext(_contextOptions))
        {
            var student = new Student.Models.Student { Id = 5, Name = "Jaaaa" };
            context.Students.Add(student);
            context.SaveChanges();
        }

        var result = await _controller.Delete(5);
        Assert.IsType<NoContentResult>(result);

        using (var context = new StudentContext(_contextOptions))
        {
            var student = await context.Students.FindAsync(5);
            Assert.Null(student);
        }
    }
}
