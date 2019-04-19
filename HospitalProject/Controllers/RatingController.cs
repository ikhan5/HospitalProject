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
//using HospitalProject.ViewModels.DoctorsList;
//using HospitalProject.Data;

namespace HospitalProject.Controllers
{
    public class RatingController : Controller
    {
        private readonly HospitalCMSContext db;

        public RatingController(HospitalCMSContext context)
        {
            db = context;
        }

        //list

        public async Task<ActionResult> Index(int pagenum)
        {
        // Pagination
            var _ratings = await db.Ratings.ToListAsync();
            int ratingCount = _ratings.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)ratingCount / perpage) - 1;
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

            List<Rating> ratings = await db.Ratings.Skip(start).Take(perpage).ToListAsync();
            return View(await db.Ratings.Include(d => d.Doctors).ToListAsync());
        }

        
        //create

        public ActionResult Create()
        {
            DoctorsList dl = new DoctorsList();
            dl.doctors = db.Doctors.ToList();
            return View(dl);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string Feedback, int DoctorID)
        {
            string insertQuery = "insert into Ratings (Feedback, DoctorID) " +
            "values (@feedback, @doctorID)";

            SqlParameter[] donparams = new SqlParameter[2];
            donparams[0] = new SqlParameter("@doctorID", DoctorID);
            donparams[1] = new SqlParameter("@feedback", Feedback);
        
            db.Database.ExecuteSqlCommand(insertQuery, donparams);
            return RedirectToAction("Index");
        }


        //edit
        public async Task<ActionResult> Edit(int id)
        {
            DoctorsList dl = new DoctorsList();
            dl.rating = db.Ratings.Include(d => d.Doctors).SingleOrDefault(d => d.RatingID == id);
            dl.doctors = db.Doctors.ToList();
            if (dl != null) return View(dl);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int RatingID, string Feedback, int DoctorID)
        {
            if (db.Ratings.Find(RatingID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Ratings set Feedback=@feedback" +
                " where RatingID=@id";
            SqlParameter[] donparams = new SqlParameter[2];
            donparams[0] = new SqlParameter("@id", RatingID);
            //donparams[1] = new SqlParameter("@doctorID", DoctorID);
            donparams[1] = new SqlParameter("@feedback", Feedback);

            db.Database.ExecuteSqlCommand(updateQuery, donparams);
            return RedirectToAction("Details/" + RatingID);
        }

        //details

        public async Task<ActionResult> Details(int id)
        {
            if (db.Ratings.Find(id) == null)
            {
                return NotFound();
            }
            DoctorsList dfl = new DoctorsList();
            dfl.rating = db.Ratings.Include(d => d.Doctors).SingleOrDefault(d => d.DoctorID == id);
            dfl.doctors = db.Doctors.ToList();
            return View(dfl);

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoctorsList dfl = new DoctorsList();
            dfl.rating = db.Ratings.Include(d => d.Doctors).SingleOrDefault(d => d.DoctorID == id);
            if (dfl == null)
            {
                return NotFound();
            }

            return View(dfl);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rating rating = await db.Ratings.FindAsync(id);

            if (rating.RatingID != id)
            {
                return Forbid();
            }

            db.Ratings.Remove(rating);
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
