using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;

namespace HMS
{

    [Authorize(Roles = "Doctor,SuperUser")]
    public class DiagnosesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Diagnoses
        public ActionResult Index()
        {
            var diagnoses = db.Diagnoses.Include(d => d.Patient);
            return View(diagnoses.ToList());
        }

        // GET: Diagnoses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }


        // GET: Diagnoses/Create
        public ActionResult Create(int id)
        {

            Debug.WriteLine("Patient id is: " + id);
            
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

        // POST: Diagnoses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClinicalRemarks,InitialDiagnosis,SecondDiagnosis,ThirdDiagnosis,PatientID")] Diagnosis diagnosis)
        {
                db.Diagnoses.Add(diagnosis);
            //find patient
             var patient =  db.Patients.Find(diagnosis.PatientID);
            if (patient != null)
            {
                patient.Diagnoses.Add(diagnosis);
                db.SaveChanges();
            }

             

                return RedirectToAction("Index");
            

            //ViewBag.PatientID = new SelectList(db.Patients, "ID", "FirstName", diagnosis.PatientID);
            //return View(diagnosis);
        }

        // GET: Diagnoses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "FirstName", diagnosis.PatientID);
            return View(diagnosis);
        }

        // POST: Diagnoses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClinicalRemarks,InitialDiagnosis,SecondDiagnosis,ThirdDiagnosis,PatientID")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "FirstName", diagnosis.PatientID);
            return View(diagnosis);
        }

        // GET: Diagnoses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            db.Diagnoses.Remove(diagnosis);
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
