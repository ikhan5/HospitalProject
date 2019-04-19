/*using System;
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
    public class DepartmentController : Controller
    {
        private readonly HospitalCMSContext db;

        public DepartmentController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Departments.ToListAsync());
        }
        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("DepartmentName")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(department);
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
            return View(department);
        }

        //edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Departments.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Departments where DepartmentID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Department mydepartment = db.Departments.FromSql(query, param).FirstOrDefault();
            return View(mydepartment);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string DepartmentName)
        {
            if ((id == null) || (db.Departments.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Departments set DepartmentName=@name" +
                " where DepartmentID=@id";
            SqlParameter[] myparams = new SqlParameter[2];
            myparams[0] = new SqlParameter("@name", DepartmentName);
            myparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

    }
}   */