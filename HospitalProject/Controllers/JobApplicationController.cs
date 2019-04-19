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
using HospitalProject.Models.JobModels;
using HospitalProject.Models.JobModels.ViewModels;

namespace HospitalProject.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly HospitalCMSContext db;

        public JobApplicationController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int pagenum)
        {

            // Pagination
            var jobApps = await db.JobApplications.ToListAsync();
            int appCount = jobApps.Count();
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

            List<JobApplication> app_pag = await db.JobApplications.Skip(start).Take(perpage).Include(j => j.JobPosting).ToListAsync();
            return View(app_pag);
        }

        // GET: JobApplications/Create
        public ActionResult Create()
        {
            ApplicationListing al = new ApplicationListing();
            al.jobPostings = db.JobPostings.ToList();
            return View(al);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string applicantName, string applicantEmail, int jobPostingID)
        {
            DateTime appDate = DateTime.Today;

            string insertQuery = "insert into JobApplications (applicantName, applicantEmail, applicationDate, jobPostingID) " +
            "values (@name,@email,@date, @postID);";
            SqlParameter[] appparams = new SqlParameter[4];
            appparams[0] = new SqlParameter("@name", applicantName);
            appparams[1] = new SqlParameter("@email", applicantEmail);
            appparams[2] = new SqlParameter("@date", appDate);
            appparams[3] = new SqlParameter("@postID", jobPostingID);

            db.Database.ExecuteSqlCommand(insertQuery, appparams);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int id)
        {
            //find job posting form where 
            ApplicationListing al = new ApplicationListing();
            al.jobApplication = db.JobApplications.Include(d => d.JobPosting)
                           .SingleOrDefault(d => d.jobApplicationID == id);
            al.jobPostings = db.JobPostings.ToList();


            if (al != null) return View(al);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int jobApplicationID, string applicantName, string applicantEmail, int jobPostingID)
        {
            if (db.JobApplications.Find(jobApplicationID) == null)
            {
                return NotFound();
            }

            DateTime appDate = DateTime.Today;

            string updateQuery = "update JobApplications set applicantName = @name, applicantEmail =@email, applicationDate=@date, jobPostingID =@postid " +
            "where jobApplicationID = @appID";
            SqlParameter[] appparams = new SqlParameter[5];
            appparams[0] = new SqlParameter("@name", applicantName);
            appparams[1] = new SqlParameter("@email", applicantEmail);
            appparams[2] = new SqlParameter("@date", appDate);
            appparams[3] = new SqlParameter("@appID", jobApplicationID);
            appparams[4] = new SqlParameter("@postID", jobPostingID);

            db.Database.ExecuteSqlCommand(updateQuery, appparams);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            if (db.JobApplications.Find(id) == null)
            {
                return NotFound();
            }
            ApplicationListing al = new ApplicationListing();
            al.jobApplication = db.JobApplications.Include(d => d.JobPosting)
                           .SingleOrDefault(d => d.jobApplicationID == id);
            al.jobPostings = db.JobPostings.ToList();
            return View(al);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            ApplicationListing al = new ApplicationListing();
            al.jobApplication = db.JobApplications.Include(d => d.JobPosting)
                           .SingleOrDefault(d => d.jobApplicationID == id);
            if (al == null)
            {
                return NotFound();
            }
            return View(al);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobApplication jobApplication = await db.JobApplications.FindAsync(id);

            if (jobApplication.jobApplicationID != id)
            {
                return Forbid();
            }

            db.JobApplications.Remove(jobApplication);
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