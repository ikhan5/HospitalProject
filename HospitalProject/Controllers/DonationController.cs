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
using HospitalProject.Models.VolunteerViewsModels;
using HospitalProject.Models.VolunteerViewsModels.ViewModels;

namespace HospitalProject.Controllers
{
    public class DonationController : Controller
    {
        private readonly HospitalCMSContext db;

        public DonationController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Donations.Include(d => d.DonationForm).ToListAsync());
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
        public async Task<ActionResult> Edit(int donationID, string donorEmail, int isRecurring, int paymentAmount, int paymentMethod, int donationFormID)
        {
            if (db.Donations.Find(donationID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Donations set donorEmail=@email, isRecurring=@recurring, paymentAmount = @amount, paymentMethod =@method" +
                " where donationID=@id AND donationFormID=@formID";
            SqlParameter[] donparams = new SqlParameter[6];
            donparams[0] = new SqlParameter("@id", donationID);
            donparams[1] = new SqlParameter("@formID", donationFormID);
            donparams[2] = new SqlParameter("@email", donorEmail);
            donparams[3] = new SqlParameter("@recurring", isRecurring);
            donparams[4] = new SqlParameter("@amount", paymentAmount);
            donparams[5] = new SqlParameter("@method", paymentMethod);


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