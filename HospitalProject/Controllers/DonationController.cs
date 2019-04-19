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
using HospitalProject.Models.DonationModels;
using HospitalProject.Models.DonationModels.ViewModels;

namespace HospitalProject.Controllers
{
    public class DonationController : Controller
    {
        private readonly HospitalCMSContext db;

        public DonationController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int pagenum)
        {

            // Pagination
            var donations = await db.Donations.ToListAsync();
            int donationCount = donations.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)donationCount / perpage) - 1;
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

            List<Donation> donations_pag = await db.Donations.Skip(start).Take(perpage).Include(d => d.DonationForm).ToListAsync();
            return View(donations_pag);
        }

        public int setTotal(int id)
        {
            DonationList dl = new DonationList();
            dl.donations = db.Donations.Where(d => d.donationFormID == id).ToList();
            var total = 0;

            total = dl.donations.Sum(i => i.paymentAmount);

            string updateAmount = "update DonationForms set totalCollected = @amount " +
                "where donationFormID=@formID";
            SqlParameter[] totalparams = new SqlParameter[2];
            totalparams[0] = new SqlParameter("@formID", id);
            totalparams[1] = new SqlParameter("@amount", total);

            db.Database.ExecuteSqlCommand(updateAmount, totalparams);

            return total;
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            DonationFormList dfl = new DonationFormList();
            dfl.donationForms = db.DonationForms.ToList();
            return View(dfl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string donorName, string donorEmail, string isRecurring, int paymentAmount, string paymentMethod, int donationFormID)
        {
            string insertQuery = "insert into Donations (donorName, donorEmail, isRecurring, paymentAmount, paymentMethod, donationFormID) " +
            "values (@name,@email,@recurring,@amount,@method, @formID)";

            SqlParameter[] donparams = new SqlParameter[6];
            donparams[0] = new SqlParameter("@formID", donationFormID);
            donparams[1] = new SqlParameter("@email", donorEmail);
            donparams[2] = new SqlParameter("@recurring", isRecurring);
            donparams[3] = new SqlParameter("@amount", paymentAmount);
            donparams[4] = new SqlParameter("@method", paymentMethod);
            donparams[5] = new SqlParameter("@name", donorName);

            db.Database.ExecuteSqlCommand(insertQuery, donparams);

            var total = setTotal(donationFormID);


            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            //find donation form where 
            DonationFormList dfl = new DonationFormList();
            dfl.donation = db.Donations.Include(d => d.DonationForm)
                           .SingleOrDefault(d => d.donationID == id);
            dfl.donationForms = db.DonationForms.ToList();

       
            if (dfl != null) return View(dfl);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int donationID, string donorEmail, string isRecurring, int paymentAmount, string paymentMethod,string donorName, int donationFormID)
        {
            if (db.Donations.Find(donationID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Donations set donorName = @name, donorEmail=@email, isRecurring=@recurring, paymentAmount = @amount, paymentMethod =@method" +
                " where donationID=@id";
            SqlParameter[] donparams = new SqlParameter[6];
            donparams[0] = new SqlParameter("@id", donationID);
            donparams[1] = new SqlParameter("@email", donorEmail);
            donparams[2] = new SqlParameter("@recurring", isRecurring);
            donparams[3] = new SqlParameter("@amount", paymentAmount);
            donparams[4] = new SqlParameter("@method", paymentMethod);
            donparams[5] = new SqlParameter("@name", donorName);

            db.Database.ExecuteSqlCommand(updateQuery, donparams);

            return RedirectToAction("Details/" + donationID);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (db.Donations.Find(id) == null)
            {
                return NotFound();
            }
            DonationFormList dfl = new DonationFormList();
            dfl.donation = db.Donations.Include(d => d.DonationForm)
                           .SingleOrDefault(d => d.donationID == id);
            dfl.donationForms = db.DonationForms.ToList();

            var DonationFormID = dfl.donation.donationFormID;

            setTotal(DonationFormID);
            return View(dfl);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            DonationFormList dfl = new DonationFormList();
            dfl.donation = db.Donations.Include(d => d.DonationForm)
                           .SingleOrDefault(d => d.donationID == id);
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
            Donation donation = await db.Donations.FindAsync(id);

            if (donation.donationID != id)
            {
                return Forbid();
            }

            db.Donations.Remove(donation);
            await db.SaveChangesAsync();


            var DonationFormID = donation.donationFormID;
            setTotal(DonationFormID);
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