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
using HospitalProject.Models.newsletter;

namespace HospitalProject.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly HospitalCMSContext db;

        public NewsletterController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _newsletter = await db.Newsletters.ToListAsync();
            int newsCount = _newsletter.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)newsCount / perpage) - 1;
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

            List<Newsletter> newsletter = await db.Newsletters.Skip(start).Take(perpage).ToListAsync();
            return View(newsletter);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("UserID, date_created, is_confirmed, news_content, news_name")] Newsletter newsletter
            )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(newsletter);
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
            return View(newsletter);
        }
        //edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Newsletters.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Newsletters where news_ID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Newsletter mynewsletter = db.Newsletters.FromSql(query, param).FirstOrDefault();
            return View(mynewsletter);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string date_created, string is_confirmed, string news_content, string news_name)
        {
            if ((id == null) || (db.Newsletters.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Newsletters set date_created=@date, is_confirmed=@confirm, news_content=@content,news_name=@newsname  " +
                
            " where news_ID=@id";
            SqlParameter[] myparams = new SqlParameter[5];
            myparams[0] = new SqlParameter("@date", date_created);
            myparams[1] = new SqlParameter("@confirm", is_confirmed);
            myparams[2] = new SqlParameter("@content", news_content);
            myparams[3] = new SqlParameter("@newsname", news_name);
            myparams[4] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }
        //details


        public async Task<ActionResult> Details(int id)
        {
            if (db.Newsletters.Find(id) == null)
            {
                return NotFound();
            }

            Newsletter newslttr = await db.Newsletters.SingleOrDefaultAsync(d => d.news_ID == id);
            return View(newslttr);
        }

        //delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Newsletter donform = db.Newsletters.Find(id);
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
            Newsletter nl = await db.Newsletters.FindAsync(id);

            if (nl.news_ID != id)
            {
                return Forbid();
            }

            db.Newsletters.Remove(nl);
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