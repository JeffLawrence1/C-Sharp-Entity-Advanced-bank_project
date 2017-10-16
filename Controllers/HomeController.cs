using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bank_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace bank_project.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;
 
        public HomeController(YourContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpGet]
        [Route("Reg")]
        public IActionResult Reg(){
            return View("Index");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model){
            if(ModelState.IsValid){

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User user = new User();

                user.Password = Hasher.HashPassword(user, model.Password);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Balance= 0;
                user.createdat = DateTime.Now;
                user.updatedat = DateTime.Now;
                
                _context.Users.Add(user);
                _context.SaveChanges();
                int UserID = _context.Users.Last().UserID;
                return RedirectToAction("Index");
            }
            else{
                return View("Register");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){

            User user = _context.Users.Where(x => x.Email == Email).SingleOrDefault();
            if(user != null && Password != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, Password)){
                    HttpContext.Session.SetInt32("UserID", user.UserID);
                    return Redirect($"ShowAccount/{user.UserID}");
                }
            }
           
            TempData["Error"] = "Wrong email or password dummy!!!! Stop trying to hack other people's accounts!!!";
            return View();
            
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
