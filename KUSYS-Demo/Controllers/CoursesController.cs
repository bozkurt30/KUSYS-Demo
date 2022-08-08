using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CoursesController(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return _context.Courses != null ?
                        View(await _context.Courses.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Courses'  is null.");
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var student = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Courses/AddOrEdit/$id
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Course());
            }
            else
            {
                var uptCourse = await _context.Courses.FindAsync(id);
                if (uptCourse == null)
                {
                    return NotFound();
                }
                return View(uptCourse);
            }
        }


        // POST: Courses/AddOrEdit/
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("CourseId,CourseName,DateCourse,Description,ImagePath")] Course course)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var path = _environment.WebRootPath;
                    var filePath = "../wwwroot/img" + course.ImagePath;
                    var fullPath=Path.Combine(path, filePath);
                    course.ImagePath = fullPath;
                    course.DateCourse = DateTime.Now;
                    _context.Add(course);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(course);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(course.CourseId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Courses.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", course) });
        }
        // POST: course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Courses.ToList()) });
        }

        private bool StudentExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
