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
using HospitalProject.Models.JobModels;

namespace HospitalProject.Controllers
{
    public class VolunteerAppController : Controller
    {
        private readonly HospitalCMSContext db;

        public JobApplication volunteerapplication { get; private set; }

        public VolunteerAppController(HospitalCMSContext context)
        {
            db = context;
        }

        //View
        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _apps = await db.VolunteerApplications.ToListAsync();
            int appCount = _apps.Count();
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

            List<VolunteerApplication> apps = await db.VolunteerApplications.Skip(start).Take(perpage).Include(d => d.VolunteerPosts).ToListAsync();

            return View(apps);
        }

        // GET: VolunteerApp/Create
        public ActionResult Create()
        {
            VolunteerPostList vpl = new VolunteerPostList();
            vpl.volunteerposts = db.VolunteerPosts.ToList();
            return View(vpl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DateTime AppDate, string AppFName, string AppLName, string Phone, string Email, int Age, string Descriptions, int VolunteerPostID)
        {
            string insertQuery = "insert into VolunteerApplications(AppDate,AppFName,AppLName,Phone,Email,Age,Descriptions,VolunteerPostID) " +
            "values (@appdate,@appfname,@applname,@phone,@email,@age,@description,@volunteerpostID)";

            SqlParameter[] myparams = new SqlParameter[8];
            myparams[0] = new SqlParameter("@appdate", AppDate);
            myparams[1] = new SqlParameter("@appfname", AppFName);
            myparams[2] = new SqlParameter("@applname", AppLName);
            myparams[3] = new SqlParameter("@phone", Phone);
            myparams[4] = new SqlParameter("@email", Email);
            myparams[5] = new SqlParameter("@age", Age);
            myparams[6] = new SqlParameter("@description", Descriptions);
            myparams[7] = new SqlParameter("@volunteerpostID", VolunteerPostID);


            db.Database.ExecuteSqlCommand(insertQuery, myparams);
            return RedirectToAction("Index");
        }

        // edit application
        public async Task<ActionResult> Edit(int id)
        {
            //find volunteer post
            VolunteerPostList vpl = new VolunteerPostList();
            vpl.volunteerapplications = db.VolunteerApplications.Include(d => d.VolunteerPosts)
                .SingleOrDefault(d => d.VolunteerAppID == id);
            vpl.volunteerposts = db.VolunteerPosts.ToList();

            if (vpl != null) return View(vpl);
            else return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult> Edit(int VolunteerAppID, DateTime AppDate, string AppFName, string AppLName, string Phone, string Email, int Age, string Descriptions, int VolunteerPostID)
        {
            if (db.VolunteerApplications.Find(VolunteerAppID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update VolunteerApplications set AppDate=@appdate,AppFName=@appfname,AppLName=@applname,Phone=@phone,Email=@email,Age=@age,Descriptions=@descriptions" +
                " where VolunteerAppID=@appid AND VolunteerPostID=@postID";
            SqlParameter[] myparams = new SqlParameter[9];
            myparams[0] = new SqlParameter("@appid", VolunteerAppID);
            myparams[1] = new SqlParameter("@postID", VolunteerPostID);
            myparams[2] = new SqlParameter("@appdate", AppDate);
            myparams[3] = new SqlParameter("@appfname", AppFName);
            myparams[4] = new SqlParameter("@applname", AppLName);
            myparams[5] = new SqlParameter("@phone", Phone);
            myparams[6] = new SqlParameter("@email", Email);
            myparams[7] = new SqlParameter("@age", Age);
            myparams[8] = new SqlParameter("@descriptions", Descriptions);


            db.Database.ExecuteSqlCommand(updateQuery, myparams);
            return RedirectToAction("Details/" + VolunteerAppID);
        }


        // details
        public async Task<ActionResult> Details(int id)
        {
            if (db.VolunteerApplications.Find(id) == null)
            {
                return NotFound();
            }
            VolunteerPostList vpl = new VolunteerPostList();
            vpl.volunteerapplications = db.VolunteerApplications.Include(d => d.VolunteerPosts)
                .SingleOrDefault(d => d.VolunteerAppID == id);
            vpl.volunteerposts = db.VolunteerPosts.ToList();
            if (vpl != null) return View(vpl);
            else return NotFound();
        }


        //delete application
        public async Task<IActionResult> Delete(int id)
        {
            VolunteerApplication volunteerapplications = await db.VolunteerApplications.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            VolunteerPostList vpl = new VolunteerPostList();
            vpl.volunteerapplications = db.VolunteerApplications.Include(d => d.VolunteerPosts)
                .SingleOrDefault(d => d.VolunteerAppID == id);
            if (vpl == null)
            {
                return NotFound();
            }
            return View(vpl);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VolunteerApplication volunteerapplications = await db.VolunteerApplications.FindAsync(id);

            if (volunteerapplications.VolunteerAppID != id)
            {
                return Forbid();
            }

            db.VolunteerApplications.Remove(volunteerapplications);
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