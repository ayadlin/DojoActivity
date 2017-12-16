using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoActivity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace DojoActivity.Controllers
{
    public class ActivitiesController : Controller
    {
        private DojoActivityContext _context;
        private User ActiveUser 
        {
            get{ return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();}
        }

        public ActivitiesController(DojoActivityContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Activity")]
        public IActionResult Index()
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            
            

            Dashboard dashData = new Dashboard
            {
                Activities = _context.Activities.Include(w => w.Guests).ToList(),
                User = ActiveUser
            };
        return View(dashData);
        }

        [HttpPost]
        [Route("newActivity")]
        public IActionResult newActivity(ActivityViewModels model)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            if(ModelState.IsValid)
            {
                Activity NewActivity = new Activity
                {
                    Title = model.Title,
                    Date = model.Date,
                    Time = model.Time,
                    Units  = model.Units,
                    Duration = model.Duration,
                    CoordinatorId = ActiveUser.UserId,
                    Description = model.Description,
                    CreatedAt =DateTime.Now,
                    UpdatedAt =DateTime.Now


                };
                _context.Activities.Add(NewActivity);
                _context.SaveChanges();
                return RedirectToAction("Index","Activity");
            }
            return View("Create");
        }

            [HttpGet]
            [Route("activity/rsvp/{id}")]
            public IActionResult Rsvp(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            RSVP rsvp = new RSVP
            {
                UserId = ActiveUser.UserId,
                ActivityId = id
            };
            _context.RSVPs.Add(rsvp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("activity/unrsvp/{id}")]
        public IActionResult UnRsvp(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            RSVP toDelete = _context.RSVPs.Where(r => r.ActivityId == id)
                                          .Where(r => r.UserId == ActiveUser.UserId)
                                          .SingleOrDefault();
            _context.RSVPs.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("activity/show/{id}")]
        public IActionResult Show(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
               return RedirectToAction("Index");
            Activity myQuery =_context.Activities.Include(w => w.Guests).ThenInclude(g => g.Guest).Where(w => w.ActivityId == id).SingleOrDefault();
            //ViewBag.Address = myQuery.Address;
            return View(myQuery);
        }

        [HttpGet, Route("Activity/Create")]
        public IActionResult ShowCreateActivityPage(){
            return View("Create");
        }

        [HttpGet]
        [Route("activity/delete/{id}")]
        public IActionResult Delete( int id)
        {
            Activity toDelete = _context.Activities.Where(w => w.ActivityId == id)
                                          .SingleOrDefault();
            _context.Activities.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");

        } 

    }
}
