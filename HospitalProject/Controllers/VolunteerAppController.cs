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
    public class VolunteerAppController : Controller
    {
        private readonly HospitalCMSContext db;

        public VolunteerAppController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.VolunteerApplications.ToListAsync());
        }

        // GET: VolunteerApp/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("AppFName,AppLName,Phone,Email,Age,Descriptions")] VolunteerApplication volunteerapp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(volunteerapp);
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
            return View(volunteerapp);
        }

        // edit application
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.VolunteerPosts.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from VolunteerApplications where VolunteerAppID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            VolunteerApplication myvolunteerapplication = db.VolunteerApplications.FromSql(query, param).FirstOrDefault();
            return View(myvolunteerapplication);
        }

        [HttpPost]
        public ActionResult Edit(int? id, DateTime AppDate, string AppFName, string AppLName, string Descriptions, string Email, string Phone)
        {
            if ((id == null) || (db.VolunteerApplications.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update VolunteerApplications set AppDate=@appdate, AppFName=@appfname, AppLName=@applname, Descriptions=@descriptions, Email=@email, Phone=@phone" +
                " where VolunteerAppID=@id";
            SqlParameter[] myparams = new SqlParameter[7];
            myparams[0] = new SqlParameter("@appdate", AppDate);
            myparams[1] = new SqlParameter("@appfname", AppFName);
            myparams[2] = new SqlParameter("@applname", AppLName);
            myparams[3] = new SqlParameter("@descriptions", Descriptions);
            myparams[4] = new SqlParameter("@email", Email);
            myparams[5] = new SqlParameter("@phone", Phone);
            myparams[6] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

        // details
        public async Task<ActionResult> Details(int id)
        {
            if (db.VolunteerApplications.Find(id) == null)
            {
                return NotFound();
            }

            VolunteerApplication volunteerapplication = await db.VolunteerApplications.SingleOrDefaultAsync(d => d.VolunteerAppID == id);
            return View(volunteerapplication);
        }


        // delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VolunteerApplication donform = db.VolunteerApplications.Find(id);
            if (donform == null)
            {
                return NotFound();
            }

            return View(donform);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VolunteerApplication volunteerapplication = await db.VolunteerApplications.FindAsync(id);

            if (volunteerapplication.VolunteerAppID != id)
            {
                return Forbid();
            }

            db.VolunteerApplications.Remove(volunteerapplication);
            await db.SaveChangesAsync();
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