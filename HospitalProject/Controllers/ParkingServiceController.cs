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
    public class ParkingServiceController : Controller
    {
        private readonly HospitalCMSContext db;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParkingServiceController(HospitalCMSContext context, IHostingEnvironment env, UserManager<ApplicationUser> usermanager)
        {
            db = context;
            _env = env;       
            _userManager = usermanager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = db.ParkingServices.AsNoTracking().OrderBy(s => s.ParkingNumber);
            var model = await PagingList.CreateAsync(query, 5, page);
            return View(model);
        }

        // GET: ParkingService/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("ParkingNumber, ParkingServiceUserID, Rate, Status")] ParkingService parkingservice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(parkingservice);
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
            return View(parkingservice);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParkingService parking = db.ParkingServices.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int ParkingServiceID, int ParkingNumber, string Rate, bool Status, string ParkingServiceUserID)
        {
            if (db.PlanYourStays.Find(ParkingServiceID) == null)
            {
                return NotFound();
            }

            string updateQuery = "UPDATE ParkingServices set ParkingNumber=@parking, Rate=@rate, Status=@status, ParkingServiceUserID=@user" +
                " where ParkingServiceID=@id";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@id", ParkingServiceID);
            sqlparams[1] = new SqlParameter("@parking", ParkingNumber);
            sqlparams[2] = new SqlParameter("@rate", Rate);
            sqlparams[3] = new SqlParameter("@status", Status);
            sqlparams[4] = new SqlParameter("@user", ParkingServiceUserID);

            db.Database.ExecuteSqlCommand(updateQuery, sqlparams);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParkingService parking = db.ParkingServices.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParkingService parking = await db.ParkingServices.FindAsync(id);

            if (parking.ParkingServiceID != id)
            {
                return Forbid();
            }

            db.ParkingServices.Remove(parking);
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
