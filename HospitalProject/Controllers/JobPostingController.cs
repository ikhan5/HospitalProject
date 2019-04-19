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
    public class JobPostingController : Controller
    {
        private readonly HospitalCMSContext db;

        public JobPostingController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int pagenum)
        {
            // Pagination
            var jobPosts = await db.JobPostings.ToListAsync();
            int postCount = jobPosts.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)postCount / perpage) - 1;
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

            List<JobPosting> post_pag = await db.JobPostings.Skip(start).Take(perpage).ToListAsync();
            return View(post_pag);
        }

        // GET: JobPostings/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("jobPostingID,jobTitle,jobQualifications,jobDescription,jobSkills,jobPostingDate,jobExpiryDate")] JobPosting jobPosting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(jobPosting);
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
            return View(jobPosting);
        }

        public async Task<ActionResult> Edit(int id)
        {
            //find donation form where 
            var jobpost = db.JobPostings.Find(id);
            if (jobpost != null) return View(jobpost);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int jobPostingID, string jobTitle, string jobQualifications, string jobDescription, string jobSkills, DateTime jobPostingDate, DateTime jobExpiryDate)
        {
            if (db.JobPostings.Find(jobPostingID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update JobPostings set jobTitle=@title, jobQualifications=@qual,jobDescription=@desc, jobSkills = @skills, jobPostingDate =@post, jobExpiryDate = @expiry" +
                " where jobPostingID=@id";
            SqlParameter[] postparams = new SqlParameter[7];
            postparams[0] = new SqlParameter("@id", jobPostingID);
            postparams[1] = new SqlParameter("@title", jobTitle);
            postparams[2] = new SqlParameter("@qual", jobQualifications);
            postparams[3] = new SqlParameter("@skills", jobSkills);
            postparams[4] = new SqlParameter("@post", jobPostingDate);
            postparams[5] = new SqlParameter("@expiry", jobExpiryDate);
            postparams[6] = new SqlParameter("@desc", jobDescription);

            db.Database.ExecuteSqlCommand(updateQuery, postparams);
            return RedirectToAction("Details/" + jobPostingID);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (db.JobPostings.Find(id) == null)
            {
                return NotFound();
            }

            ApplicantsListing al = new ApplicantsListing();
            al.jobApplications = db.JobApplications.Where(j => j.jobPostingID == id).ToList();
            al.jobPosting = await db.JobPostings.SingleOrDefaultAsync(j => j.jobPostingID == id);
            return View(al);
        }
       
                public async Task<IActionResult> Delete(int id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    JobPosting jobpost = db.JobPostings.Find(id);
                    if (jobpost == null)
                    {
                        return NotFound();
                    }

                    return View(jobpost);
                }
        
                // POST: Authors/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> DeleteConfirmed(int id)
                {
                    JobPosting jobPosting = await db.JobPostings.FindAsync(id);

                    if (jobPosting.jobPostingID != id)
                    {
                        return Forbid();
                    }

                    db.JobPostings.Remove(jobPosting);
                    await db.SaveChangesAsync();
                    /*      SqlParameter donparam = new SqlParameter("@id", id);
                            string deleteQuery = "delete from DonationForms where donationFormID=@id;";
                            deleteQuery += "delete from Donations where donationFormID=@id";
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