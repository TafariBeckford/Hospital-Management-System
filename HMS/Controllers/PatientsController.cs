using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;

namespace HMS
{
   [ Authorize(Roles="Doctor,Administrator,Receptionist")]
    public class PatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Patients
        public ActionResult Index()
        {
            var list = db.Patients.ToList();
            return View(list);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Include(s => s.Files).SingleOrDefault(s => s.ID == id); 
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //// GET: Patients/PatientDocs
        public ActionResult PatientDocs(int id)
        {
            var patient = new Patient();
            if (id < 0)
            {
                return View("Error");
            }

            if (db.Patients.Find(id) != null)
            {
                patient = db.Patients.Find(id);

            }
            ViewBag.PatientId = patient.ID;
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientDocs(Patient patient, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var docs = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,

                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        docs.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    patient.Files = new List<File> { docs };
                }
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }













        // GET: Patients/Create
        public ActionResult Create()
        {
            List<Parishes> ListParishes = new List<Parishes>()
            {
                new Parishes() {ID = 1, Parish="Kingston" },
                new Parishes() {ID = 2, Parish="St.Andrew" },
                new Parishes() {ID = 3, Parish="St.Thomas" },
                new Parishes() {ID = 4, Parish="Portland" },
                new Parishes() {ID = 5, Parish="St.Mary" },
                new Parishes() {ID = 6, Parish="St.Ann" },
                new Parishes() {ID = 7, Parish="Trelawny"},
                new Parishes() {ID = 8, Parish="St.James"},
                new Parishes() {ID = 9, Parish="Hanover"},
                new Parishes() {ID = 10, Parish="Westmoreland"},
                new Parishes() {ID = 11, Parish="St.Elizabeth"},
                new Parishes() {ID = 12, Parish="Manchester"},
                new Parishes() {ID = 13, Parish="Clarendon"},
                new Parishes() {ID = 14, Parish="St.Catherine" },

            };
            // Retrieve Parish and build SelectList
            ViewBag.Parish = new SelectList(ListParishes, "Parish", "Parish");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,Address,Town,Parish,DOB")] Patient patient, HttpPostedFileBase  upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                    
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    patient.Files = new List<File> {avatar};
                }
                db.Patients.Add(patient);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload,Patient model)
        {

            //if (id == null)
            //{ 
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var patientToUpdate = db.Patients.Find(model.ID);
            if (patientToUpdate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patientToUpdate.Address = model.Address;
            patientToUpdate.Parish = model.Parish;
            patientToUpdate.FirstName = model.FirstName;
            patientToUpdate.LastName = model.LastName;
            patientToUpdate.Town = model.Town;
            patientToUpdate.Gender = model.Gender;
            patientToUpdate.DOB = model.DOB;


            if (upload != null && upload.ContentLength > 0)
            {
                if (patientToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                {
                    db.Files.Remove(patientToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                }
                var avatar = new File
                {
                    FileName = System.IO.Path.GetFileName(upload.FileName),
                    FileType = FileType.Avatar,
                    ContentType = upload.ContentType
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    avatar.Content = reader.ReadBytes(upload.ContentLength);
                }
                patientToUpdate.Files = new List<File> { avatar };
            }
            db.Entry(patientToUpdate).State = EntityState.Modified;
            db.SaveChanges();
           
            return RedirectToAction("Index");
            
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
