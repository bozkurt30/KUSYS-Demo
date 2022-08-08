using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<SelectCoursesOfStudent> SelectCoursesOfStudents { get; set; }
    }
}
