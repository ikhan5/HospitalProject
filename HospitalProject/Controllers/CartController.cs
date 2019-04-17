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
using HospitalProject.Models.GiftShop.ViewModel;

namespace HospitalProject.Controllers
{
    public class CartController : Controller
    {
        private readonly HospitalCMSContext db;

        public CartController(HospitalCMSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Carts.ToListAsync());
        }


        public ActionResult Create()
        {
            BillingList bl = new BillingList();
            bl.billings = db.Billings.ToList();
            
            return View(bl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int Total, string Paymentmethod, int Billingid)
        {
            string query = "insert into Carts (BillingID, Paymentmethods, Total )" +
                "values (@billingid, @methods, @total)";

            SqlParameter[] billparam = new SqlParameter[3];
            billparam[0] = new SqlParameter("@billingid", Billingid);
            billparam[1] = new SqlParameter("@methods", Paymentmethod);
            billparam[2] = new SqlParameter("@total", Total);
            db.Database.ExecuteSqlCommand(query, billparam);
            Debug.Write(query);
            return RedirectToAction("Index");

        }
    }
}