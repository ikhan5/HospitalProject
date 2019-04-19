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
using HospitalProject.Models.VolunteerViews;
using System.Data;

namespace HospitalProject.Controllers
{
    public class VolunteerPostController : Controller
    {
        private readonly HospitalCMSContext db;

        public SqlDbType VolunteerAppID { get; private set; }

        public VolunteerPostController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.VolunteerPosts.ToListAsync());
        }

        // add post
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("VolunteerPostID,PostDate,Position,Department,Details")] VolunteerPost volunteerpost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(volunteerpost);
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
            return View(volunteerpost);
        }

        // edit post
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.VolunteerPosts.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from VolunteerPosts where VolunteerPostID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            VolunteerPost myvolunteerpost = db.VolunteerPosts.FromSql(query, param).FirstOrDefault();
            return View(myvolunteerpost);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string Department, string Details, string Position, DateTime PostDate)
        {
            if ((id == null) || (db.VolunteerPosts.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update VolunteerPosts set Department=@department, Details=@details, Position=@position" +
                " where VolunteerPostID=@id";
            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@department", Department);
            myparams[1] = new SqlParameter("@details", Details);
            myparams[2] = new SqlParameter("@position", Position);
            myparams[3] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

        // details
        public async Task<ActionResult> Details(int id)
        {
            if (db.VolunteerPosts.Find(id) == null)
            {
                return NotFound();
            }

            VolunteerPost volunteerpost = await db.VolunteerPosts.SingleOrDefaultAsync(d => d.VolunteerPostID == id);
            return View(volunteerpost);
        }


        // delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VolunteerPost donform = db.VolunteerPosts.Find(id);
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
            VolunteerPost volunteerpost = await db.VolunteerPosts.FindAsync(id);

            if (volunteerpost.VolunteerPostID != id)
            {
                return Forbid();
            }

            db.VolunteerPosts.Remove(volunteerpost);
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