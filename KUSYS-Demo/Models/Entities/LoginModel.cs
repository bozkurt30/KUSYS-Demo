using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models.Entities
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Olamaz")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş girilemez")]
        public string Password { get; set; }
    }
}
