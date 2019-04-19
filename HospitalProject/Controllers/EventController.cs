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
using HospitalProject.Models.GiftShop;
using HospitalProject.Models.Events;

namespace HospitalProject.Controllers
{
    public class EventController : Controller
    {
        private readonly HospitalCMSContext db;

        public EventController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _event = await db.Events.ToListAsync();
            int eventCount = _event.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)eventCount / perpage) - 1;
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

            List<Event> eve = await db.Events.Skip(start).Take(perpage).ToListAsync();
            return View(eve);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("UserID, event_date, event_descp, event_name")] Event events)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(events);
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
            return View(events);
        }
        //edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Events.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Events where EventID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Event myevent = db.Events.FromSql(query, param).FirstOrDefault();
            return View(myevent);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string UserID, string event_date, string event_descp, string event_name)
        {
            if ((id == null) || (db.Events.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Events set UserID=@uID, event_date=@date, event_descp=@descp,event_name=@name  " +

            " where EventID=@id";
            SqlParameter[] myparams = new SqlParameter[5];
            myparams[0] = new SqlParameter("@uID", UserID);
            myparams[1] = new SqlParameter("@date", event_date);
            myparams[2] = new SqlParameter("@descp", event_descp);
            myparams[3] = new SqlParameter("@name", event_name);
            myparams[4] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }
        //details


        public async Task<ActionResult> Details(int id)
        {
            if (db.Events.Find(id) == null)
            {
                return NotFound();
            }

            Event evnt = await db.Events.SingleOrDefaultAsync(d => d.EventID == id);
            return View(evnt);
        }

        //delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event donform = db.Events.Find(id);
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
            Event eve = await db.Events.FindAsync(id);

            if (eve. EventID!= id)
            {
                return Forbid();
            }

            db.Events.Remove(eve);
            await db.SaveChangesAsync();
            /*	SqlParameter donparam = new SqlParameter("@id", id);
            	string deleteQuery = "delete from DonationForms where donationFormID=@id";
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