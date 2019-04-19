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
using ReflectionIT.Mvc.Paging;

namespace HospitalProject.Controllers
{
    public class PlanYourStayController : Controller
    {
        private readonly HospitalCMSContext db;

        public PlanYourStayController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = db.PlanYourStays.AsNoTracking().OrderBy(s => s.ServiceName);
            var model = await PagingList.CreateAsync(query, 5, page);
            return View(model);
        }

        // GET: PlanYourStay/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("PlanYourStayUserID, RatePerDay, RoomNumber, ServiceName")] PlanYourStay planyourstay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(planyourstay);
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
            return View(planyourstay);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlanYourStay plan = db.PlanYourStays.Find(id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int PlanYourStayID, string RatePerDay, string RoomNumber, string ServiceName, string PlanYourStayUserID)
        {
            if (db.PlanYourStays.Find(PlanYourStayID) == null)
            {
                return NotFound();
            }

            string updateQuery = "UPDATE PlanYourStays set RatePerDay=@rate, RoomNumber=@room, ServiceName=@service, PlanYourStayUserID=@user" +
                " where PlanYourStayID=@id";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@id", PlanYourStayID);
            sqlparams[1] = new SqlParameter("@rate", RatePerDay);
            sqlparams[2] = new SqlParameter("@room", RoomNumber);
            sqlparams[3] = new SqlParameter("@service", ServiceName);
            sqlparams[4] = new SqlParameter("@user", PlanYourStayUserID);

            db.Database.ExecuteSqlCommand(updateQuery, sqlparams);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlanYourStay plan = db.PlanYourStays.Find(id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PlanYourStay plan = await db.PlanYourStays.FindAsync(id);

            if (plan.PlanYourStayID != id)
            {
                return Forbid();
            }

            db.PlanYourStays.Remove(plan);
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