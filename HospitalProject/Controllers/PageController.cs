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
    public class PageController : Controller
    {
        private readonly HospitalCMSContext db;

        public PageController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Pages.Include(p => p.Navigation).ToListAsync());
        }

        // GET: Page/Create
        public ActionResult Create()
        {
            NavigationListing nl = new NavigationListing();
            nl.navigations = db.Navigations.ToList();
            return View(nl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("pageID,pageAuthor,pageTitle,pageContent,dateCreated,lastModified,pageOrder,navigationID")] Page page)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(page);
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
            return View(page);
        }

        public async Task<ActionResult> Edit(int id)
        {
            //find donation form where 
            NavigationListing nl = new NavigationListing();
            nl.page = db.Pages.Include(d => d.Navigation)
                           .SingleOrDefault(d => d.pageID == id);
            nl.navigations = db.Navigations.ToList();


            if (nl != null) return View(nl);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int pageID, string pageAuthor, string pageTitle, string pageContent, DateTime dateCreated, DateTime lastModified, int pageOrder, int navigationID)
        {
            if (db.Pages.Find(pageID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Pages set pageAuthor =@author,pageTitle =@title,pageContent=@content,dateCreated=@created,lastModified=@modified,pageOrder=@order,navigationID=@nav" +
                " where pageID=@id";
            SqlParameter[] pageparams = new SqlParameter[8];
            pageparams[0] = new SqlParameter("@author", pageAuthor);
            pageparams[1] = new SqlParameter("@title", pageTitle);
            pageparams[2] = new SqlParameter("@content", pageContent);
            pageparams[3] = new SqlParameter("@created", dateCreated);
            pageparams[4] = new SqlParameter("@modified", lastModified);
            pageparams[5] = new SqlParameter("@order", pageOrder);
            pageparams[6] = new SqlParameter("@nav", navigationID);
            pageparams[7] = new SqlParameter("@id", pageID);


            db.Database.ExecuteSqlCommand(updateQuery, pageparams);

            return RedirectToAction("Details/" + pageID);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (db.Pages.Find(id) == null)
            {
                return NotFound();
            }
            NavigationListing nl = new NavigationListing();
            nl.page = db.Pages.Include(d => d.Navigation)
                           .SingleOrDefault(d => d.pageID == id);
            nl.navigations = db.Navigations.ToList();

            return View(nl);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            NavigationListing nl = new NavigationListing();
            nl.page = db.Pages.Include(d => d.Navigation)
                           .SingleOrDefault(d => d.pageID == id);
            if (nl == null)
            {
                return NotFound();
            }
            return View(nl);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Page page = await db.Pages.FindAsync(id);

            if (page.pageID != id)
            {
                return Forbid();
            }

            db.Pages.Remove(page);
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