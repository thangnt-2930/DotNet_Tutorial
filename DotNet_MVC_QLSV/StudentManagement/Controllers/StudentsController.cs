using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;
using System.Threading.Tasks;

namespace Student.Controllers;

public class StudentsController : Controller
{
    private readonly StudentContext _context;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(StudentContext context, ILogger<StudentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        return View(await _context.Students.ToListAsync());
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Models.Student student)
    {
        _context.Add(student);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Student created successfully: {@Student}", student);
        return RedirectToAction(nameof(Index));
    }

    // GET: Students/Edit/5
    [CheckDataNotFoundAttribute("id")]
    public async Task<IActionResult> Edit(int? id)
    {
        var student = await _context.Students.FindAsync(id);
        return View(student);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Models.Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(student.Id))
            {
                return NotFound();
            }
            else
            {
                return View(student);
            }
        }
    }

    // GET: Students/Delete/5
    [CheckDataNotFoundAttribute("id")]
    public async Task<IActionResult> Delete(int? id)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.Id == id);

        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> CourseOfStudent(int studentId)
    {
        var courses = await _context.Enrollments
                    .Where(e => e.StudentId == studentId)
                    .Select(e => e.Course)
                    .ToListAsync();
        
        return View(courses);
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
