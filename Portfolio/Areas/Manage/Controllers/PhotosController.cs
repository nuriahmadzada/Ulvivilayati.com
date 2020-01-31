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
    public class PhotosController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();
        public ActionResult Index()
        {
            List<Photo> photos = db.Photos.ToList();
            return View(photos);
        }
    }
}