using HMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
   
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        
        //public JsonResult GetEvents()
        //{
        //    using (ApplicationDbContext dc = new ApplicationDbContext())
        //    {
        //        var events = dc.Events.ToList();
        //        return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //}

        //[HttpPost]
        //public JsonResult SaveEvent(Event e)
        //{
        //    var status = false;
        //    using (ApplicationDbContext dc = new ApplicationDbContext())
        //    {
        //        if (e.EventID > 0)
        //        {
        //            //Update the event
        //            var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
        //            if (v != null)
        //            {
        //                v.Subject = e.Subject;
        //                v.Start = e.Start;
        //                v.End = e.End;
        //                v.Description = e.Description;
        //                v.IsFullDay = e.IsFullDay;
        //                v.ThemeColor = e.ThemeColor;
        //            }
        //        }
        //        else
        //        {
        //            dc.Events.Add(e);
        //        }

        //        dc.SaveChanges();
        //        status = true;

        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}

        //[HttpPost]
        //public JsonResult DeleteEvent(int eventID)
        //{
        //    var status = false;
        //    using (ApplicationDbContext dc = new ApplicationDbContext())
        //    {
        //        var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
        //        if (v != null)
        //        {
        //            dc.Events.Remove(v);
        //            dc.SaveChanges();
        //            status = true;
        //        }
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}


        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image 
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }

        }
    }
}