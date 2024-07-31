using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Course.Models;
using System.Threading.Tasks;
using Student.Data;
using System.Diagnostics;

namespace Course.Controllers;

public class CoursesController : Controller
{
    private readonly StudentContext _context;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(StudentContext context, ILogger<CoursesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
        return View(await _context.Courses.ToListAsync());
    }

    // GET: Courses/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Course.Models.Course Course)
    {
        _context.Add(Course);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Course created successfully: {@Course}", Course);
        return RedirectToAction(nameof(Index));
    }

    // GET: Courses/Edit/5
    // [CheckDataNotFoundAttribute("id")]
    public async Task<IActionResult> Edit(int? id)
    {
        var course = await _context.Courses.FindAsync(id);
        return View(course);
    }

    // POST: Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Course.Models.Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        try
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CourseExists(course.Id))
            {
                return NotFound();
            }
            else
            {
                return View(course);
            }
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Courses/Delete/5
    // [CheckDataNotFoundAttribute("id")]
    public async Task<IActionResult> Delete(int? id)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(m => m.Id == id);

        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> StudentInCourse(int courseId)
    {
        var students = await _context.Enrollments
                    .Where(e => e.CourseId == courseId)
                    .Select(e => e.Student)
                    .ToListAsync();
        
        return View(students);
    }

    private bool CourseExists(int id)
    {
        return _context.Courses.Any(e => e.Id == id);
    }
}
