using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using System.Data.Entity;
using Portfolio.Areas.Helpers;

namespace Portfolio.Areas.Manage.Controllers
{
    [AuthLogin]
    public class DashboardController : Controller
    {
        PorfolioEntities db = new PorfolioEntities();

        public ActionResult Index()
        {
            Info info = new Info();
            info = db.Infos.FirstOrDefault();
            return View(info);
        }

        public ActionResult Edit(int id)
        {
            Info infos = db.Infos.Where(f => f.Id == id).FirstOrDefault();
            if(infos != null)
            {
                return View(infos);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(Info info, HttpPostedFileBase ProfilePhoto, HttpPostedFileBase BgPhoto)
        {
            Info TheInfo = db.Infos.FirstOrDefault(f => f.Id == info.Id);
            if(TheInfo != null)
            {
                if(ProfilePhoto != null)
                {
                    string filePath = Server.MapPath("~/Upload/" + TheInfo.ProfilePhoto);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ProfilePhoto.FileName.Replace(" ", "_");
                    string path = System.IO.Path.Combine(Server.MapPath("~/Upload"), filename);

                    ProfilePhoto.SaveAs(path);
                    TheInfo.ProfilePhoto = filename;
                }

                if (BgPhoto != null)
                {
                    string bgpath = Server.MapPath("~/Upload/" + TheInfo.BgPhoto);
                    if (System.IO.File.Exists(bgpath))
                    {
                        System.IO.File.Delete(bgpath);
                    }

                    string bgfilename = DateTime.Now.ToString("yyyyMMddHHmmss") + BgPhoto.FileName.Replace(" ", "_");
                    string path = System.IO.Path.Combine(Server.MapPath("~/Upload"), bgfilename);

                    BgPhoto.SaveAs(path);
                    TheInfo.BgPhoto = bgfilename;
                }

                TheInfo.Name = info.Name;
                TheInfo.TitleFirst = info.TitleFirst;
                TheInfo.TitleSecond = info.TitleSecond;
                TheInfo.TitleThird = info.TitleThird;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            if(Request.Cookies["cookie"] != null)
            {
                HttpCookie mycookie = new HttpCookie("cookie")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(mycookie);
            }
            return RedirectToAction("index", "login");
        }
    }
}