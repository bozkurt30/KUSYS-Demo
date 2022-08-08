using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Lütfen soyadınızı girin")]
        [StringLength(100)]
        [Display(Name = "Kurs Adı ")]
        public string? CourseName { get; set; }
        [Required(ErrorMessage = "Lütfen Resim Ekleyiniz")]
        [StringLength(1000)]
        [Display(Name = "Resim ")]
        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Açıklama Alanı Boş Girilemez")]
        [StringLength(100)]
        [Display(Name = "Açıklama ")]
        public string? Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Tarih ")]
        public DateTime DateCourse { get; set; }
        //public Student Students { get; set; }
    }
}
