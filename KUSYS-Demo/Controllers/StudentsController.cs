using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo.Models;
using KUSYS_Demo.Models.Entities;

namespace KUSYS_Demo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Students
        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                        View(await _context.Students.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }
            var firstName = await _context.Students.Where(c=>c.StudentId==id).Select(c=>c.FirstName).FirstOrDefaultAsync();
            var lastName = await _context.Students.Where(c=>c.StudentId == id).Select(c=>c.LastName).FirstOrDefaultAsync();
            var username = await _context.Students.Where(c=>c.StudentId == id).Select(c=>c.Username).FirstOrDefaultAsync();
            ViewBag.UserFullName = firstName + " " + lastName;
            ViewBag.userName = username;
            var viewModelList = (from p in _context.Students
                                 join o in _context.SelectCoursesOfStudents on p.Username equals o.Username
                                 join c in _context.Courses on o.CoursesId equals c.CourseId into temp
                                 from t in temp.DefaultIfEmpty()
                                 select new ViewModelList
                                 {
                                     id = p.StudentId,
                                     FullName = p.FirstName + " " + p.LastName,
                                     CourseName = t.CourseName,
                                     Description = t.Description,
                                     UserDate = p.BirthDate,
                                     CourseDate = t.DateCourse,
                                     ImagePath = t.ImagePath,
                                     Username = p.Username
                                 });
            var snc = await viewModelList.Where(c => c.id == id).ToListAsync();
            if (snc.Count == 0)
            {
                TempData["uyarı"] = "Herhangi bir kurs seçilmemiş...";
            }

            return View(snc);
        }
        public IActionResult UserDetailCourse(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var viewModelList = (from p in _context.Students
                                 join o in _context.SelectCoursesOfStudents on p.Username equals o.Username
                                 join c in _context.Courses on o.CoursesId equals c.CourseId into temp
                                 from t in temp.DefaultIfEmpty()
                                 select new ViewModelList
                                 {
                                     id = p.StudentId,
                                     FullName = p.FirstName + " " + p.LastName,
                                     CourseName = t.CourseName,
                                     Description = t.Description,
                                     UserDate = p.BirthDate,
                                     CourseDate = t.DateCourse,
                                     ImagePath = t.ImagePath,
                                     Username = p.Username
                                 });
            if (viewModelList == null)
            {
                return NotFound();
            }

            return View(viewModelList);
        }

        // GET: Students/AddOrEdit/$id
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Student());
            }
            else
            {
                var uptStudent = await _context.Students.FindAsync(id);
                if (uptStudent == null)
                {
                    return NotFound();
                }
                return View(uptStudent);
            }
        }


        // POST: Students/AddOrEdit/
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("StudentId,FirstName,LastName,BirthDate,Username,Password")] Student student)
        {
            var result = GetUserByName(student.Username);
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    if (!result)
                    {
                        student.BirthDate = DateTime.Now;
                        _context.Add(student);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return Json(new { isValid = false, result, html = Helper.RenderRazorViewToString(this, "AddOrEdit", student) });
                    }
                }
                else
                {
                    try
                    {
                        Student updateStudent = await _context.Students.FindAsync(id);
                        updateStudent.Password = student.Password;
                        updateStudent.FirstName = student.FirstName;
                        updateStudent.LastName = student.LastName;
                        updateStudent.BirthDate = DateTime.Now;
                        _context.Update(updateStudent);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(student.StudentId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Students.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", student) });
        }
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student= await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Students.ToList()) });
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
        public bool GetUserByName(string username)
        {
            var usernam = _context.Students.Where(x => x.Username == username).FirstOrDefault();
            if (usernam == null) { return false; }
            return true;
        }
    }
}
