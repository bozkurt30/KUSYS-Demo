using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KUSYS_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            return _context.Courses != null ?
                        View(_context.Courses.ToList()) :
                        Problem("Entity set 'AppDbContext.Courses'  is null.");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> SelectCourse(int id)
        {
            bool resultSelect = false;
            if (ModelState.IsValid)
            {
                if (id != 0)
                {
                    var courseInfo = _context.Courses.Where(c => c.CourseId == id).FirstOrDefault();
                    var username = HttpContext.Session.GetString("username");

                    var isValidCourse = _context.SelectCoursesOfStudents.Where(c => c.CoursesId == id && c.Username == username).FirstOrDefault();
                    if (isValidCourse ==null)
                    {
                        SelectCoursesOfStudent selectCoursesOf = new SelectCoursesOfStudent()
                        {
                            CoursesId = id,
                            Username = username,
                        };

                        _context.Update(selectCoursesOf);
                        await _context.SaveChangesAsync();
                        resultSelect = true;
                    }
                    else
                    {
                        bool sonuc = true;
                        return Json(new { isValid = true, resultSelect,sonuc });
                    }
                    
                }
                else
                {
                    return Json(new { isValid = false});
                }
                return Json(new { isValid = true, resultSelect });
            }
            return Json(new { isValid = false });
        }
    }
}