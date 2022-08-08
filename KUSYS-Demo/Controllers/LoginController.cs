using KUSYS_Demo.Models;
using KUSYS_Demo.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginModel student)
        {
            if (student != null)
            {
                //string M_KULLANICI_SIFRE = Encrypted.MD5Olustur(u.M_KULLANICI_SIFRE);
               
                var us = _context.Students.Where(x => x.Username == student.Username && x.Password == student.Password).SingleOrDefault();
                if ((student.Username == "admin" || student.Username == "Admin") && student.Password == "admin")
                {
                    HttpContext.Session.SetString("username", student.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (us != null)
                    {
                        HttpContext.Session.SetString("username", us.Username);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        TempData["error"] = "Kullanıcı Adı veya Şifre Yanlış Girildi";
                        return RedirectToAction("Index", "Login");

                    }
                }
                
                //return View();
            }
            else
            {
                TempData["error"] = "Kullanıcı Adı veya Şifre Yanlış Girildi";
                return RedirectToAction("Index", "Login");
            }
        }
        [Route("logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Login");
        }
    }
}
