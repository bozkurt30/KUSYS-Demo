using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public int CoursetId { get; set; }
        [Required(ErrorMessage ="Lütfen adınızı girin")]
        [StringLength(100)]
        [Display(Name ="Adı ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen soyadınızı girin")]
        [StringLength(100)]
        [Display(Name = "Soyadı ")]
        public string LastName { get; set; }
       
        [Required]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        [Display(Name = "Tarih ")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "Lütfen Kullanıcı adını benzersiz girin")]
        [StringLength(100)]
        [Display(Name = "Kullanıcı Adı ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre girin")]
        [StringLength(100)]
        [Display(Name = "Şifre ")]
        public string Password { get; set; }


        //public ICollection<Course> Courses { get; set; } = default;
    }
}
