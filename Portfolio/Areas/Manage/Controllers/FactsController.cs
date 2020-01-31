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
    public class FactsController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Fact> facts = db.Facts.ToList();
            return View(facts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Fact fact)
        {
            db.Facts.Add(fact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            Fact fact = db.Facts.Where(f => f.Id == Id).FirstOrDefault();
            if (fact != null)
            {
                return View(fact);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(Fact fact)
        {
            Fact TheFact = db.Facts.Where(f => f.Id == fact.Id).FirstOrDefault();
            if (TheFact != null)
            {
                TheFact.Icon = fact.Icon;
                TheFact.Title = fact.Title;
                TheFact.Count = fact.Count;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult Remove(int? Id)
        {
            if(Id == null)
            {
                return HttpNotFound();
            }
            Fact fact = db.Facts.Find(Id);
            db.Facts.Remove(fact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}