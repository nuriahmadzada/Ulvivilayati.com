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
    public class CategoriesController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Where(f => f.Id == id).FirstOrDefault();

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            Category TheCategory = db.Categories.Where(f => f.Id == category.Id).FirstOrDefault();

            if (TheCategory != null)
            {
                TheCategory.Name = category.Name;
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
            Category category = db.Categories.Find(Id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}