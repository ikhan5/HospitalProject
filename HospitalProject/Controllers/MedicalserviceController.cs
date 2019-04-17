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

namespace HospitalProject.Controllers
{
    public class MedicalserviceController : Controller
    {
        private readonly HospitalCMSContext db;

        public MedicalserviceController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _ms = await db.Medicalservices.ToListAsync();
            int msCount = _ms.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)msCount / perpage) - 1;
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

            List<Medicalservice> msq = await db.Medicalservices.Skip(start).Take(perpage).ToListAsync();
            return View(msq);
        }


        // GET: Medicalservice/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("medical_service_type, medical_services_description,medical_services_name")] Medicalservice medicalservice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(medicalservice);
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
            return View(medicalservice);
        }

        // GET: Medicalservice/Edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Medicalservices.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Medicalservices where medical_services_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Medicalservice mservices = db.Medicalservices.FromSql(query, param).FirstOrDefault();
            return View(mservices);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string medical_service_type, string medical_services_description, string medical_services_name)
        {
            if ((id == null) || (db.Medicalservices.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Medicalservices set medical_service_type=@medicalserviceType, medical_services_description=@medicalserviceDesc, medical_services_name=@medicalserviceName" +
                " where medical_services_id=@id";
            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@medicalserviceType", medical_service_type);
            myparams[1] = new SqlParameter("@id", id);
            myparams[2] = new SqlParameter("@medicalserviceDesc", medical_services_description);
            myparams[3] = new SqlParameter("@medicalserviceName", medical_services_name);


            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");

        }
        //Medical service/details
        public async Task<ActionResult> Details(int id)
        {
            if (db.Medicalservices.Find(id) == null)
            {
                return NotFound();
            }

            Medicalservice msDetails = await db.Medicalservices.SingleOrDefaultAsync(d => d.medical_services_id == id);
            return View(msDetails);
        }

        //Medical service/delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Medicalservice ms = db.Medicalservices.Find(id);
            if (ms == null)
            {
                return NotFound();
            }

            return View(ms);
        }

        // POST: Medical service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicalservice msDelete = await db.Medicalservices.FindAsync(id);

            if (msDelete.medical_services_id != id)
            {
                return Forbid();
            }

            db.Medicalservices.Remove(msDelete);
            await db.SaveChangesAsync();
            /*	SqlParameter donparam = new SqlParameter("@id", id);
            	string deleteQuery = "delete from Appointments where client_id=@id";
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