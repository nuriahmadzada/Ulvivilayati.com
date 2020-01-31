using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Portfolio.Models;
using Portfolio.Areas.Helpers;

namespace Portfolio.Areas.Manage.Controllers
{
    [AuthLogin]
    public class AboutsController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();

        public ActionResult Index()
        {
            About about = db.Abouts.FirstOrDefault();
            return View(about);
        }

        public ActionResult Edit(int Id)
        {
            About about = db.Abouts.Where(f => f.Id == Id).FirstOrDefault();
            if(about != null)
            {
                return View(about);
            }
            return RedirectToAction("index");
        }

        
        [HttpPost]
        public ActionResult Edit(About about)
        {
            About TheAbout = db.Abouts.Where(f=>f.Id == about.Id).FirstOrDefault();

            if(TheAbout != null)
            {
                TheAbout.Title = about.Title;
                TheAbout.Description = about.Description;
                TheAbout.Birthday = about.Birthday;
                TheAbout.Residence = about.Residence;
                TheAbout.Email = about.Email;
                TheAbout.Phone = about.Phone;
                TheAbout.Behance = about.Behance;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}