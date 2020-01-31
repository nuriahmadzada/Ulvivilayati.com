using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Portfolio.Areas.Helpers;

namespace Portfolio.Areas.Manage.Controllers
{
    [AuthLogin]
    public class ProjectsController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Project> projects = db.Projects.ToList();
            return View(projects);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project, List<HttpPostedFileBase> Photos, string[] Technologies)
        {
            db.Projects.Add(project);
            db.SaveChanges();
            List<TechnologyProject> TechnologyProjectss = new List<TechnologyProject>();
            foreach (var tech in Technologies)
            {
                TechnologyProject technologyProject = new TechnologyProject();
                technologyProject.ProjectId = project.Id;
                technologyProject.Technology = tech;
                TechnologyProjectss.Add(technologyProject);
            }
            db.TechnologyProjects.AddRange(TechnologyProjectss);

            foreach (var photos in Photos)
            {

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + photos.FileName.Replace(" ", "_");
                    string path = System.IO.Path.Combine(Server.MapPath("~/Upload"), filename);
                    photos.SaveAs(path);
                    Photo photo = new Photo();
                    photo.PhotoName = filename;
                    photo.ProjectId = project.Id;
                    db.Photos.Add(photo);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            Project project = db.Projects.FirstOrDefault(u => u.Id == Id);
            ViewBag.Categories = db.Categories.ToList();
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(Project project, List<HttpPostedFileBase> Photos, string[] Technologies)
        {
            Project TheProject = db.Projects.Where(u=>u.Id == project.Id).FirstOrDefault();
            if(TheProject != null)
            {
                TheProject.Name = project.Name;
                TheProject.Customer = project.Customer;
                TheProject.Date = project.Date;
                TheProject.Category = project.Category;
                TheProject.Description = project.Description;
                TheProject.Web = project.Web;
                db.SaveChanges();
            }

            List<TechnologyProject> technologyProjects = new List<TechnologyProject>();
            technologyProjects = db.TechnologyProjects.Where(u => u.ProjectId == TheProject.Id).ToList();
            db.TechnologyProjects.RemoveRange(technologyProjects);

            if(Photos.FirstOrDefault()?.ContentLength > 0)
            {
                foreach (var photos in Photos)
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + photos.FileName.Replace(" ", "_");
                    string path = System.IO.Path.Combine(Server.MapPath("~/Upload"), filename);
                    photos.SaveAs(path);
                    Photo photo = new Photo();
                    photo.PhotoName = filename;
                    photo.ProjectId = project.Id;
                    db.Photos.Add(photo);
                }
            }

            

            List<TechnologyProject> TechnologyProjectss = new List<TechnologyProject>();
            foreach (var tech in Technologies)
            {
                TechnologyProject technologyProject = new TechnologyProject();
                technologyProject.ProjectId = project.Id;
                technologyProject.Technology = tech;
                TechnologyProjectss.Add(technologyProject);
            }
            db.TechnologyProjects.AddRange(TechnologyProjectss);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.FirstOrDefault(f => f.Id == id);
            if (photo != null)
            {
                db.Photos.Remove(photo);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Remove(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            Project project = db.Projects.Find(Id);
            List<Photo> photos = db.Photos.Where(u => u.ProjectId == Id).ToList();
            List<TechnologyProject> technologyProjects = db.TechnologyProjects.Where(u => u.ProjectId == Id).ToList();
            db.Photos.RemoveRange(photos);
            db.TechnologyProjects.RemoveRange(technologyProjects);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}