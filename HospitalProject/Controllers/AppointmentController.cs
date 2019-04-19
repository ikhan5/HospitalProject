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
using HospitalProject.Models.ViewModels;


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
            // return View(app);
            return View(await db.Appointments.Include(a => a.Doctors).ToListAsync());
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            AppointmentList app = new AppointmentList();
            app.doctors = db.Doctors.ToList();
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string appointment_details, string client_emailadd, string client_fname, string client_lname, int client_phone, string date_time, int DoctorID)
        {
            string insertQuery = "insert into Appointments (appointment_details, client_emailadd, client_fname, client_lname, client_phone,date_time, DoctorID) " +
            "values (@app_details,@client_email,@client_fname,@client_lname, @client_phn,@app_datetime,@doctorID)";

            SqlParameter[] donparams = new SqlParameter[7];
            donparams[0] = new SqlParameter("@app_details", appointment_details);
            donparams[1] = new SqlParameter("@doctorID", DoctorID);
            donparams[2] = new SqlParameter("@client_email", client_emailadd);
            donparams[3] = new SqlParameter("@client_fname", client_fname);
            donparams[4] = new SqlParameter("@client_lname", client_lname);
            donparams[5] = new SqlParameter("@client_phn", client_phone);
            donparams[6] = new SqlParameter("@app_datetime", date_time);

            db.Database.ExecuteSqlCommand(insertQuery, donparams);
            return RedirectToAction("Index");
        }


        // GET: Appointment/Edit
        /*  public ActionResult Edit(int? id)
          {
              if ((id == null) || (db.Appointments.Find(id) == null))
              {
                  return NotFound();
              }
              string query = "select * from Appointments where client_id=@id";
              SqlParameter param = new SqlParameter("@id", id);
              Appointment app = db.Appointments.FromSql(query, param).FirstOrDefault();
              return View(app);
          }*/

        public async Task<ActionResult> Edit(int id)
        {
            AppointmentList app = new AppointmentList();
            app.appointment = db.Appointments.Include(d => d.Doctors)
                           .SingleOrDefault(d => d.DoctorID == id);
            app.doctors = db.Doctors.ToList();
            if (app != null) return View(app);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int client_id, string appointment_details, string client_emailadd, string client_fname, string client_lname,int client_phone, string date_time, int DoctorID)
        {
            if (db.Appointments.Find(client_id) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Appointments set appointment_details=@details, client_emailadd=@email, client_fname = @fname, client_lname = @lname, client_phone=@phn, date_time=@datetime" +
                " where client_id=@id AND DoctorID=@docID";
            SqlParameter[] donparams = new SqlParameter[8];
            donparams[0] = new SqlParameter("@id", client_id);
            donparams[1] = new SqlParameter("@docID", DoctorID);
            donparams[2] = new SqlParameter("@details", appointment_details);
            donparams[3] = new SqlParameter("@email", client_emailadd);
            donparams[4] = new SqlParameter("@fname", client_fname);
            donparams[5] = new SqlParameter("@lname", client_lname);
            donparams[6] = new SqlParameter("@phn", client_phone);
            donparams[7] = new SqlParameter("@date_time", date_time);


            db.Database.ExecuteSqlCommand(updateQuery, donparams);
            return RedirectToAction("Details/" + client_id);
            //return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            if (db.Appointments.Find(id) == null)
            {
                return NotFound();
            }
            AppointmentList dfl = new AppointmentList();
            dfl.appointment = db.Appointments.Include(d => d.Doctors)
                           .SingleOrDefault(d => d.DoctorID == id);
            dfl.doctors = db.Doctors.ToList();
            return View(dfl);
        }

        //Appointment/details
        /* public async Task<ActionResult> Details(int id)
         {
             if (db.Appointments.Find(id) == null)
             {
                 return NotFound();
             }

             Appointment appDetails = await db.Appointments.SingleOrDefaultAsync(d => d.client_id == id);
             return View(appDetails);
         }*/

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