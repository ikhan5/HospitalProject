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

        public async Task<IActionResult> Index()
        {
            return View(await db.ParkingServices.ToListAsync());
        }

        // GET: Authors/Create
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
    }

}
