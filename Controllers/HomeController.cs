using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoActivity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DojoActivity.Controllers
{
    public class HomeController : Controller
    {
      private DojoActivityContext _context;
      private User ActiveUser 
        {
            get{ return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("USerId")).FirstOrDefault();}
        }
        
        public HomeController(DojoActivityContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModels model)
        {
            if (_context.Users.Where(u => u.Email == model.Email).SingleOrDefault() != null)
            {
                ModelState.AddModelError("Username", "Username in use");
            }
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User NewUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                NewUser.Password = Hasher.HashPassword(NewUser, model.Password);
                _context.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", NewUser.UserId);
                return RedirectToAction("Index", "Activity");
            }
            else
            {
                return View("Index");
            }

        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModels model)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            User userToLog = _context.Users.Where(u => u.Email == model.LogEmail).SingleOrDefault();
            if (userToLog == null)
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
            else if (model.LogPassword == null || hasher.VerifyHashedPassword(userToLog, userToLog.Password, model.LogPassword) == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
            }
            if (!ModelState.IsValid)
                return View("Index");
            HttpContext.Session.SetInt32("UserId", userToLog.UserId);
            return RedirectToAction("Index", "Activity");
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Display/{id}")]
        public IActionResult Show(int id)
        {
            if (HttpContext.Session.GetInt32("USerId") == null ||
              HttpContext.Session.GetInt32("USerId") != id)
            {
                return RedirectToAction("Index");
            }
            return View(this.ActiveUser);

        }
    }

}
