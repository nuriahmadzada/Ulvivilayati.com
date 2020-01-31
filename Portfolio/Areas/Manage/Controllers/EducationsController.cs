using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Areas.Helpers;
using Portfolio.Models;

namespace Portfolio.Areas.Manage.Controllers
{
    [AuthLogin]
    public class EducationsController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Education> educations = db.Educations.ToList();
            return View(educations);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Education education)
        {
            db.Educations.Add(education);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Education education = db.Educations.Where(f => f.Id == id).FirstOrDefault();

            if(education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        [HttpPost]
        public ActionResult Edit(Education education)
        {
            Education TheEducation = db.Educations.Where(f=>f.Id == education.Id).FirstOrDefault();

            if(TheEducation != null)
            {
                TheEducation.Year = education.Year;
                TheEducation.Place = education.Place;
                TheEducation.Faculty = education.Faculty;
                TheEducation.Description = education.Description;
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
            Education education = db.Educations.Find(Id);
            db.Educations.Remove(education);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}