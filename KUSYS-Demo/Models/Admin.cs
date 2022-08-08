using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Bilgileri girin")]
        [StringLength(100)]
        [Display(Name = "Adı ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen Kullanıcı adını benzersiz girin")]
        [StringLength(100)]
        [Display(Name = "Kullanıcı Adı ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre girin")]
        [StringLength(100)]
        [Display(Name = "Şifre ")]
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
