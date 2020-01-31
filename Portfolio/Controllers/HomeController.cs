using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.ViewModel;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            ViewDatas model = new ViewDatas
            {
                Info = db.Infos.FirstOrDefault(),
                About = db.Abouts.FirstOrDefault(),
                Services = db.Services.ToList(),
                Facts = db.Facts.ToList(),
                Educations = db.Educations.ToList(),
                Experiences = db.Experiences.ToList(),
                Skills = db.Skills.ToList(),
                Categories = db.Categories.ToList(),
                Projects = db.Projects.ToList(),
                Photos = db.Photos.ToList(),
                TechnologyProjects = db.TechnologyProjects.ToList()
            };
            return View(model);
        }

        public PartialViewResult Portfolio(int Id)
        {

            return PartialView("Portfolio");
        }
    }
}