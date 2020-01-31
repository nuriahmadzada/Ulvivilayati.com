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
    public class ServicesController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Service> services = db.Services.ToList();
            return View(services);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Service services)
        {
            db.Services.Add(services);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            Service service = db.Services.Where(f => f.Id == Id).FirstOrDefault();
            if (service != null)
            {
                return View(service);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(Service service)
        {
            Service TheService = db.Services.Where(f => f.Id == service.Id).FirstOrDefault();
            if (TheService != null)
            {
                TheService.Name = service.Name;
                TheService.Description = service.Description;
                TheService.Icon = service.Icon;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int? Id)
        {
            if(Id == null)
            {
                return HttpNotFound();
            }

            Service service = db.Services.Find(Id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}