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
    public class DonationFormController : Controller
    {
        private readonly HospitalCMSContext db;

        public DonationFormController(HospitalCMSContext context)
        {
            db = context;
        }

      
        public async Task<IActionResult> Index()
        {
            return View(await db.DonationForms.ToListAsync());
        }

        // GET: DonationForms/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("donationFormID,donationCause,charityName,donationGoal,presetAmounts,formDescription, totalCollected")] DonationForm donationForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(donationForm);
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
            return View(donationForm);
        }

        public async Task<ActionResult> Edit(int id)
        {
            //find donation form where 
            var donForm = db.DonationForms.Find(id);
            if (donForm != null) return View(donForm);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int donationFormID, string donationCause, string charityName, int donationGoal, string presetAmounts, string formDescription)
        {
            if (db.DonationForms.Find(donationFormID)==null)
            {
                return NotFound();
            }

            string updateQuery = "update DonationForms set donationCause=@cause, charityName=@name, donationGoal = @goal, presetAmounts =@presets, formDescription = @desc" +
                " where donationFormID=@id";
            SqlParameter[] donparams = new SqlParameter[6];
            donparams[0] = new SqlParameter("@id", donationFormID);
            donparams[1] = new SqlParameter("@cause", donationCause);
            donparams[2] = new SqlParameter("@name", charityName);
            donparams[3] = new SqlParameter("@goal", donationGoal);
            donparams[4] = new SqlParameter("@presets", presetAmounts);
            donparams[5] = new SqlParameter("@desc", formDescription);

            db.Database.ExecuteSqlCommand(updateQuery, donparams);
            return RedirectToAction("Details/" + donationFormID);
        }

        public async Task<ActionResult> Details (int id)
        {
            if (db.DonationForms.Find(id) == null)
            {
                return NotFound();
            }

            DonationList dl = new DonationList();
            dl.donations = db.Donations.Where(d=>d.donationFormID == id).ToList();
            dl.donationForm = await db.DonationForms.SingleOrDefaultAsync(d => d.donationFormID == id);
     
            return View(dl);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DonationForm donform = db.DonationForms.Find(id);
            if (donform == null)
            {
                return NotFound();
            }

            return View(donform);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DonationForm donationForm = await db.DonationForms.FindAsync(id);

            if (donationForm.donationFormID != id)
            {
                return Forbid();
            }

            db.DonationForms.Remove(donationForm);
            await db.SaveChangesAsync();
        /*      SqlParameter donparam = new SqlParameter("@id", id);
                string deleteQuery = "delete from DonationForms where donationFormID=@id;";
                deleteQuery += "delete from Donations where donationFormID=@id";
                await db.Database.ExecuteSqlCommandAsync(deleteQuery, donparam);
        */
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