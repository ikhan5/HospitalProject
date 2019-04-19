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
    public class ClinicController : Controller
    {
        private readonly HospitalCMSContext db;

        public ClinicController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _clinic = await db.Clinics.ToListAsync();
            int clinicCount = _clinic.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)clinicCount / perpage) - 1;
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

            List<Clinic> clinic = await db.Clinics.Skip(start).Take(perpage).ToListAsync();
            return View(clinic);
        }


        // GET: Clinic/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("clinic_description, clinic_location,clinic_name,clinic_phone,clinic_services")] Clinic clinic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(clinic);
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
            return View(clinic);
        }
        // GET: Clinic/Edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Clinics.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Clinics where clinic_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Clinic app = db.Clinics.FromSql(query, param).FirstOrDefault();
            return View(app);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string clinic_description, string clinic_location, string clinic_name, string clinic_phone, string clinic_services)
        {
            if ((id == null) || (db.Clinics.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Clinics set clinic_description=@clinicDescription, clinic_location=@clinicLocation, clinic_name=@clinicName, clinic_phone=@clinicPhone, clinic_services=@clinicServices" +
                " where clinic_id=@id";
            SqlParameter[] myparams = new SqlParameter[6];
            myparams[0] = new SqlParameter("@clinicDescription", clinic_description);
            myparams[1] = new SqlParameter("@id", id);
            myparams[2] = new SqlParameter("@clinicLocation", clinic_location);
            myparams[3] = new SqlParameter("@clinicName", clinic_name);
            myparams[4] = new SqlParameter("@clinicPhone", clinic_phone);
            myparams[5] = new SqlParameter("@clinicServices", clinic_services);
            

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
            
        }

        //Clinic/details
        public async Task<ActionResult> Details(int id)
        {
            if (db.Clinics.Find(id) == null)
            {
                return NotFound();
            }

            Clinic clinicDetails = await db.Clinics.SingleOrDefaultAsync(d => d.clinic_id == id);
            return View(clinicDetails);
        }

        //Clinic/delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clinic app = db.Clinics.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Clinic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clinic clinicDelete = await db.Clinics.FindAsync(id);

            if (clinicDelete.clinic_id != id)
            {
                return Forbid();
            }

            db.Clinics.Remove(clinicDelete);
            await db.SaveChangesAsync();
            /*	SqlParameter donparam = new SqlParameter("@id", id);
            	string deleteQuery = "delete from Appointments where client_id=@id";
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