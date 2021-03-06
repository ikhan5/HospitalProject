﻿using System;
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
    public class ReferAPatientController : Controller
    {
        private readonly HospitalCMSContext db;

        public ReferAPatientController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _referrals = await db.ReferAPatients.ToListAsync();
            int referralCount = _referrals.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)referralCount / perpage) - 1;
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

            List<ReferAPatient> referrals = await db.ReferAPatients.Skip(start).Take(perpage).ToListAsync();
            return View(referrals);
        }

        // Create referapatient
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("MedHist,CurrPrimDiag,DOB,OHIP,PatAddress,PatEmail,PatName,PatPhone,ProgReq,ReferFac,ReferPhysEmail,ReferPhysName,ReferPhysPhone,ReferalDate,ServiceReq")]  ReferAPatient referapatient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(referapatient);
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
            return View(referapatient);
        }

        // edit referapatient
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.ReferAPatients.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from ReferAPatients where ReferAPatientID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            ReferAPatient myReferAPatient = db.ReferAPatients.FromSql(query, param).FirstOrDefault();
            return View(myReferAPatient);
        }

        [HttpPost]
        public ActionResult Edit(int? id, DateTime ReferalDate, string PatName, DateTime DOB, string OHIP, 
            string PatAddress, string PatPhone, string PatEmail, string ProgReq, string CurrPrimDiag, 
            string MedHist, string ServiceReq, string ReferFac, string ReferPhysName, string ReferPhysPhone, 
            string ReferPhysEmail)
        {
            if ((id == null) || (db.ReferAPatients.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update ReferAPatients set ReferalDate=@referaldate, PatName=@patname, DOB=@dob, OHIP=@ohip, PatAddress=@pataddress, PatPhone=@patphone, PatEmail=@patemail , ProgReq=@progreq, CurrPrimDiag=@currprimdiag, MedHist=@medhist, ServiceReq=@servicereq, ReferFac=@referfac, ReferPhysName=@referphysname, ReferPhysPhone=@referphysphone,ReferPhysEmail=@referphysemail" +
                " where ReferAPatientID=@id";
            SqlParameter[] myparams = new SqlParameter[16];
            myparams[0] = new SqlParameter("@referaldate", ReferalDate);
            myparams[1] = new SqlParameter("@patname", PatName);
            myparams[2] = new SqlParameter("@dob", DOB);
            myparams[3] = new SqlParameter("@ohip", OHIP);
            myparams[4] = new SqlParameter("@pataddress", PatAddress);
            myparams[5] = new SqlParameter("@patphone", PatPhone);
            myparams[6] = new SqlParameter("@patemail", PatEmail);
            myparams[7] = new SqlParameter("@progreq", ProgReq);
            myparams[8] = new SqlParameter("@currprimdiag", CurrPrimDiag);
            myparams[9] = new SqlParameter("@medhist", MedHist);
            myparams[10] = new SqlParameter("@servicereq", ServiceReq);
            myparams[11] = new SqlParameter("@referfac", ReferFac);
            myparams[12] = new SqlParameter("@referphysname", ReferPhysName);
            myparams[13] = new SqlParameter("@referphysphone", ReferPhysPhone);
            myparams[14] = new SqlParameter("@referphysemail", ReferPhysEmail);
            myparams[15] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

        // details referapatient
        public async Task<ActionResult> Details(int id)
        {
            if (db.ReferAPatients.Find(id) == null)
            {
                return NotFound();
            }

            ReferAPatient referapatient = await db.ReferAPatients.SingleOrDefaultAsync(d => d.ReferAPatientID == id);
            return View(referapatient);
        }


        // delete referapatient
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReferAPatient donform = db.ReferAPatients.Find(id);
            if (donform == null)
            {
                return NotFound();
            }

            return View(donform);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReferAPatient referapatient = await db.ReferAPatients.FindAsync(id);

            if (referapatient.ReferAPatientID != id)
            {
                return Forbid();
            }

            db.ReferAPatients.Remove(referapatient);
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