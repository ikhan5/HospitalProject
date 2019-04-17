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
    public class AppointmentController : Controller
    {
        private readonly HospitalCMSContext db;

        public AppointmentController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _app = await db.Appointments.ToListAsync();
            int appCount = _app.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)appCount / perpage) - 1;
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

            List<Appointment> app = await db.Appointments.Skip(start).Take(perpage).ToListAsync();
            return View(app);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("appointment_details, client_doctor_id,client_emailadd,client_fname,client_lname,client_phone,date_time")] Appointment appointment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(appointment);
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
            return View(appointment);
        }

        // GET: Appointment/Edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Appointments.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Appointments where client_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Appointment app = db.Appointments.FromSql(query, param).FirstOrDefault();
            return View(app);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string appointment_details, string client_doctor_id, string client_emailadd, string client_fname, string client_lname, string client_phone, string date_time)
        {
            if ((id == null) || (db.Appointments.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Appointments set appointment_details=@appointmentDetails, client_doctor_id=@clientDocid, client_emailadd=@emailAdd, client_fname=@clientFname, client_lname=@clientLname, client_phone=@clientPhone, date_time=@appDateTime" +
                " where client_id=@id";
            SqlParameter[] myparams = new SqlParameter[8];
            myparams[0] = new SqlParameter("@appointmentDetails", appointment_details);
            myparams[1] = new SqlParameter("@id", id);
            myparams[2] = new SqlParameter("@clientDocid", client_doctor_id);
            myparams[3] = new SqlParameter("@emailAdd", client_emailadd);
            myparams[4] = new SqlParameter("@clientFname", client_fname);
            myparams[5] = new SqlParameter("@clientLname", client_lname);
            myparams[6] = new SqlParameter("@clientPhone", client_phone);
            myparams[7] = new SqlParameter("@appDateTime", date_time);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

        //Appointment/details
        public async Task<ActionResult> Details(int id)
        {
            if (db.Appointments.Find(id) == null)
            {
                return NotFound();
            }

            Appointment appDetails = await db.Appointments.SingleOrDefaultAsync(d => d.client_id == id);
            return View(appDetails);
        }

        //Appointment/delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment app = db.Appointments.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Appointment appDelete = await db.Appointments.FindAsync(id);

            if (appDelete.client_id != id)
            {
                return Forbid();
            }

            db.Appointments.Remove(appDelete);
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