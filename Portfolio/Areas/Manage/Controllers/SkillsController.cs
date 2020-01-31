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
    public class SkillsController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Skill> skills = db.Skills.ToList();
            return View(skills);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            db.Skills.Add(skill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Skill skill = db.Skills.Where(f => f.Id == id).FirstOrDefault();

            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        [HttpPost]
        public ActionResult Edit(Skill skill)
        {
            Skill TheSkill = db.Skills.Where(f => f.Id == skill.Id).FirstOrDefault();

            if (TheSkill != null)
            {
                TheSkill.Name = skill.Name;
                TheSkill.Percentage = skill.Percentage;
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
            Skill skill = db.Skills.Find(Id);
            db.Skills.Remove(skill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}