using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Enrollments.Models;
using System.Threading.Tasks;

namespace Enrollments.Controllers;

public class EnrollmentsController : Controller
{
    private readonly StudentContext _context;
    private readonly ILogger<EnrollmentsController> _logger;

    public EnrollmentsController(StudentContext context, ILogger<EnrollmentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        return View(await _context.Enrollments.ToListAsync());
    }

    // GET: Students/New
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StudentId,CourseId")] Enrollment enrollment)
    {
        if (StudentAndCourseExists(enrollment))
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Enrollment created successfully: {@enrollment}", enrollment);
            return RedirectToAction(nameof(Index));
        } else
        {
            return NotFound();
        }
    }

    private bool StudentAndCourseExists(Enrollment enrollment)
    {
        return _context.Students.Any(e => e.Id == enrollment.StudentId) && _context.Courses.Any(e => e.Id == enrollment.CourseId);
    }
}
