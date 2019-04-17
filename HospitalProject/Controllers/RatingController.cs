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
using HospitalProject.Models.ViewModels;
//using HospitalProject.ViewModels.DoctorsList;
//using HospitalProject.Data;

namespace HospitalProject.Controllers
{
    public class RatingController : Controller
    {
        private readonly HospitalCMSContext db;

        public RatingController(HospitalCMSContext context)
        {
            db = context;
        }

        //list

        public async Task<ActionResult> Index(int pagenum)
        {
        // Pagination
            var _ratings = await db.Ratings.ToListAsync();
            int ratingCount = _ratings.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)ratingCount / perpage) - 1;
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

            List<Rating> ratings = await db.Ratings.Skip(start).Take(perpage).ToListAsync();
            return View(ratings);
        }

        
        //create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("RatingID,DoctorID,Feedback")] Rating rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(rating);
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
            return View(rating);
        }

        //update

        //edit

        //delete

    }
}
