using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Controllers;
using Student.Data;

namespace StudentApi.Tests;
public class StudentsControllerTests
{
    private readonly Mock<ILogger<StudentsController>> _mockLogger;
    private readonly StudentsController _controller;
    private readonly StudentContext _context;

    public StudentsControllerTests()
    {
        var options = new DbContextOptionsBuilder<StudentContext>()
            .UseInMemoryDatabase(databaseName: "StudentDatabase")
            .Options;

        _context = new StudentContext(options);
        _mockLogger = new Mock<ILogger<StudentsController>>();

        _controller = new StudentsController(_context, _mockLogger.Object);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task Create()
    {
        var student = new Student.Models.Student { Id = 1, Name = "John Doe" };
        var result = await _controller.Create(student);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task UpdateSuccess()
    {
        var student = new Student.Models.Student { Id = 2, Name = "John Doe" };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        _context.Entry(student).State = EntityState.Detached;

        var updatedStudent = new Student.Models.Student { Id = 2, Name = "Update Test" };
        var result = await _controller.Edit(2, updatedStudent);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);

        var studentInDb = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 2);
        Assert.Equal("Update Test", studentInDb.Name);
    }

    [Fact]
    public async Task UpdateFailed()
    {
        var updatedStudent = new Student.Models.Student { Id = 1, Name = "Update Test" };

        var result = await _controller.Edit(11, updatedStudent);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete()
    {
        var student = new Student.Models.Student { Id = 1, Name = "John Doe" };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var result = await _controller.DeleteConfirmed(1);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);

        var studentInDb = await _context.Students.FindAsync(1);
        Assert.Null(studentInDb);
    }

    public async Task DeleteFailed()
    {
        var student = new Student.Models.Student { Id = 1, Name = "John Doe" };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var result = await _controller.DeleteConfirmed(2);

        Assert.IsType<NotFoundResult>(result);
    }
}
