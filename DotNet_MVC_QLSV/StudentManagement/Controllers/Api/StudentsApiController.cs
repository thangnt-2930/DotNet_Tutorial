using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StudentsApiController : ControllerBase
{
    private readonly StudentContext _context;
    public StudentsApiController(StudentContext context)
    {
        _context = context;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student.Models.Student>>> Index()
    {
        return await _context.Students.ToListAsync();
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<Student.Models.Student>> CreateStudent([FromBody] Student.Models.Student student)
    {
        if (student == null)
        {
            return BadRequest("Student object is null");
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student.Models.Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, Student.Models.Student student)
    {
        if (id != student.Id)
        {
            return BadRequest();
        }

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
