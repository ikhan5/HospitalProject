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
using HospitalProject.Models.VolunteerViewsModels.ViewModels;
using HospitalProject.Models.VolunteerPostModels.ViewModels;

namespace HospitalProject.Controllers
{
    public class VolunteerAppController : Controller
    {
        private readonly HospitalCMSContext db;

        public VolunteerAppController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            return View(await db.VolunteerApplications.Include(d=>d.VolunteerPosts).ToListAsync());
        }

        // GET: VolunteerApp/Create
        public ActionResult Create()
        {
            VolunteerPostList dfl = new VolunteerPostList();
            dfl.VolunteerPosts = db.VolunteerPosts.ToList();
            return View(dfl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DateTime PostDate,string AppFName,string AppLName,string Phone,string Email,int Age,string Descriptions,int VolunteerPostID)
        {
            string insertQuery = "insert into VolunteerApplictions (PostDate,AppFName,AppLName,Phone,Email,Age,Descriptions,VolunteerPostID) " +
            "values (@postdate,@appfname,@applname,@phone,@email,@age,@description,@volunteerpostID)";

            SqlParameter[] myparams = new SqlParameter[8];
            myparams[0] = new SqlParameter("@volunteerpostID", VolunteerPostID);
            myparams[1] = new SqlParameter("@postdate", PostDate);
            myparams[2] = new SqlParameter("@appfname", AppFName);
            myparams[3] = new SqlParameter("@applname", AppLName);
            myparams[4] = new SqlParameter("@phone", Phone);
            myparams[5] = new SqlParameter("@email", Email);
            myparams[6] = new SqlParameter("@age", Age);
            myparams[7] = new SqlParameter("@description", Descriptions);


            db.Database.ExecuteSqlCommand(insertQuery, myparams);
            return RedirectToAction("Index");
        }

        // edit application
        public async Task<ActionResult> Edit(int id)
        {
            //find donation form where 
            VolunteerPostList dfl = new VolunteerPostList();
            dfl.VolunteerApplications = db.VolunteerApplications.Include(d => d.VolunteerPosts)
                           .SingleOrDefault(d => d.VolunteerPostID == id);
            dfl.VolunteerPosts = db.VolunteerPosts.ToList();
            if (dfl != null) return View(dfl);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int VolunteerAppID,DateTime PostDate,string AppFName,string AppLName,string Phone,string Email,int Age,string Descriptions,int VolunteerPostID)
        {
            if (db.VolunteerApplications.Find(VolunteerAppID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update VolunteerApplications set PostDate=@postdate,AppFName=@appfname,AppLName=@applname,Phone=@phone,Email=@email,Age=@age,Description=@description" +
                " where VolunteerAppID=@appid AND VolunteerPostID=@postID";
            SqlParameter[] myparams = new SqlParameter[9];
            myparams[0] = new SqlParameter("@appid", VolunteerAppID);
            myparams[1] = new SqlParameter("@postID", VolunteerPostID);
            myparams[2] = new SqlParameter("@postdate", PostDate);
            myparams[3] = new SqlParameter("@appfname", AppFName);
            myparams[4] = new SqlParameter("@applname", AppLName);
            myparams[5] = new SqlParameter("@phone", Phone);
            myparams[6] = new SqlParameter("@email", Email);
            myparams[7] = new SqlParameter("@age", Age);
            myparams[8] = new SqlParameter("@description", Descriptions);


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