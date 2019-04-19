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
    public class EmergencyWaitTimeController : Controller
    {
        private readonly HospitalCMSContext db;

        public EmergencyWaitTimeController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = db.EmergencyWaitTimes.AsNoTracking().OrderBy(s => s.ServiceName);
            var model = await PagingList.CreateAsync(query, 5, page);
            return View(model);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("ServiceName, WaitTime")] EmergencyWaitTime emergencywaittime)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(emergencywaittime);
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
            return View(emergencywaittime);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmergencyWaitTime emergency = db.EmergencyWaitTimes.Find(id);
            if (emergency == null)
            {
                return NotFound();
            }

            return View(emergency);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int EmergencyWaitTimeID, string ServiceName, string WaitTime)
        {
            if (db.EmergencyWaitTimes.Find(EmergencyWaitTimeID) == null)
            {
                return NotFound();
            }

            string updateQuery = "UPDATE EmergencyWaitTimes set ServiceName=@service, WaitTime=@time" +
                " where EmergencyWaitTimeID=@id";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@id", EmergencyWaitTimeID);
            sqlparams[1] = new SqlParameter("@service", ServiceName);
            sqlparams[2] = new SqlParameter("@time", WaitTime);

            db.Database.ExecuteSqlCommand(updateQuery, sqlparams);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmergencyWaitTime emergency = db.EmergencyWaitTimes.Find(id);
            if (emergency == null)
            {
                return NotFound();
            }

            return View(emergency);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EmergencyWaitTime emergency = await db.EmergencyWaitTimes.FindAsync(id);

            if (emergency.EmergencyWaitTimeID != id)
            {
                return Forbid();
            }

            db.EmergencyWaitTimes.Remove(emergency);
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