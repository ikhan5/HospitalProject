using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HospitalProject.Models;
using HospitalProject.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HospitalCMSContext db;

        public DoctorController(HospitalCMSContext context)
        {
            db = context;
        }

       /* public async Task<IActionResult> Index(int pagenum)
        {
            return View(await db.Doctors.ToListAsync());
        }*/

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _doctors = await db.Doctors.ToListAsync();
            int doctorCount = _doctors.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)doctorCount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = perpage * pagenum;
            ViewData["pagenum"] = (int)pagenum;
            ViewData["PaginationSummary"] = "";
            if (maxpage > 0)
            {
                ViewData["PaginationSummary"] =
                    (pagenum + 1).ToString() + " of " +
                    (maxpage + 1).ToString();
            }

            List<Doctor> doctors = await db.Doctors.Skip(start).Take(perpage).ToListAsync();
            return View(doctors);
        }
        
        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("DoctorName,DoctorDepartment,DoctorDescription")] Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(doctor);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(doctor);
        }



        //edit

        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Doctors.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Doctors where DoctorID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Doctor mydoctor = db.Doctors.FromSql(query, param).FirstOrDefault();
            return View(mydoctor);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string DoctorDepartment, string DoctorDescription, string DoctorName)
        {
            if ((id == null) || (db.Doctors.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Doctors set DoctorDepartment=@doctordepartment, DoctorDescription=@doctordescription, DoctorName=@doctorname" +
                " where DoctorID=@id";
            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@doctordepartment", DoctorDepartment);
            myparams[1] = new SqlParameter("@doctordescription", DoctorDescription);
            myparams[2] = new SqlParameter("@doctorname", DoctorName);
            myparams[3] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }
        

        //details
       

        public async Task<ActionResult> Details(int id)
        {
            if (db.Doctors.Find(id) == null)
            {
                return NotFound();
            }

            Doctor doctor = await db.Doctors.SingleOrDefaultAsync(d => d.DoctorID == id);
            return View(doctor);
        }


        //delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Doctor donform = db.Doctors.Find(id);
            if (donform == null)
            {
                return NotFound();
            }

            return View(donform);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Doctor doctor = await db.Doctors.FindAsync(id);

            if (doctor.DoctorID != id)
            {
                return Forbid();
            }

            db.Doctors.Remove(doctor);
            await db.SaveChangesAsync();
            /*	SqlParameter donparam = new SqlParameter("@id", id);
            	string deleteQuery = "delete from DonationForms where donationFormID=@id";
            	await db.Database.ExecuteSqlCommandAsync(deleteQuery, donparam);*/
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