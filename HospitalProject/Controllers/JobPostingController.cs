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
using HospitalProject.Models.JobModels;

namespace HospitalProject.Controllers
{
    public class JobPostingController : Controller
    {
        private readonly HospitalCMSContext db;

        public JobPostingController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.JobPostings.ToListAsync());
        }

        // GET: JobPostings/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("jobPostingID,jobTitle,jobQualifications,jobDescription,jobSkills,jobPostingDate,jobExpiryDate")] JobPosting jobPosting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(jobPosting);
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
            return View(jobPosting);
        }


    }
}