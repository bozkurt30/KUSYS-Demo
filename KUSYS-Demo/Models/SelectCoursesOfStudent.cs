using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class SelectCoursesOfStudent
    {
        [Key]
        public int Id { get; set; }
        public int CoursesId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }
    }
}
