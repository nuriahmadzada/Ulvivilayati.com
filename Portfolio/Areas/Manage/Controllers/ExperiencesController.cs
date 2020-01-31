using Portfolio.Areas.Helpers;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Areas.Manage.Controllers
{
    [AuthLogin]
    public class ExperiencesController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Experience> experiences = db.Experiences.ToList();
            return View(experiences);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Experience experience = db.Experiences.Where(f => f.Id == id).FirstOrDefault();

            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        [HttpPost]
        public ActionResult Edit(Experience experience)
        {
            Experience TheExperience = db.Experiences.Where(f => f.Id == experience.Id).FirstOrDefault();

            if (TheExperience != null)
            {
                TheExperience.Date = experience.Date;
                TheExperience.Company = experience.Company;
                TheExperience.Position = experience.Position;
                TheExperience.Description = experience.Description;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            Experience experience = db.Experiences.Find(Id);
            db.Experiences.Remove(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}