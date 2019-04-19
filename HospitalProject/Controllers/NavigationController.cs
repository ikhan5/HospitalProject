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
using HospitalProject.Models.MVPModels;
using HospitalProject.Models.MVPModels.ViewModels;

namespace HospitalProject.Controllers
{
    public class NavigationController : Controller
    {
        private readonly HospitalCMSContext db;

        public NavigationController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Navigations.ToListAsync());
        }

        // GET: Navigation/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("navigationID,navigationName,navigationURL,navigationPosition")] Navigation navigation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(navigation);
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
            return View(navigation);
        }

        public async Task<ActionResult> Edit(int id)
        {
            //find donation form where 
            var nav = db.Navigations.Find(id);
            if (nav != null) return View(nav);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int navigationID, string navigationName, string navigationURL, int navigationPosition)
        {
            if (db.Navigations.Find(navigationID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Navigations set navigationName=@name, navigationURL=@url, navigationPosition = @pos" +
                " where navigationID=@id";
            SqlParameter[] navparam = new SqlParameter[4];
            navparam[0] = new SqlParameter("@id", navigationID);
            navparam[1] = new SqlParameter("@name", navigationName);
            navparam[2] = new SqlParameter("@url", navigationURL);
            navparam[3] = new SqlParameter("@pos", navigationPosition);

            db.Database.ExecuteSqlCommand(updateQuery, navparam);
            return RedirectToAction("Details/" + navigationID);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (db.Navigations.Find(id) == null)
            {
                return NotFound();
            }

            PageListing pl = new PageListing();
            pl.pages = db.Pages.Where(p => p.navigationID == id).ToList();
            pl.navigation = await db.Navigations.SingleOrDefaultAsync(d => d.navigationID == id);

            return View(pl);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Navigation nav = db.Navigations.Find(id);
            if (nav == null)
            {
                return NotFound();
            }

            return View(nav);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Navigation nav = await db.Navigations.FindAsync(id);

            if (nav.navigationID != id)
            {
                return Forbid();
            }

            db.Navigations.Remove(nav);
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