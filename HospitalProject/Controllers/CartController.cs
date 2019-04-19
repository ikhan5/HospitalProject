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

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
            var _cart = await db.Carts.ToListAsync();
            int cartCount = _cart.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)cartCount / perpage) - 1;
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

            List<Cart> eve = await db.Carts.Skip(start).Take(perpage).ToListAsync();
            return View(eve);
        }


        public ActionResult Create()
        {
           
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int Total, string Paymentmethods)
        {
            string query = "insert into Carts (Paymentmethods, Total )" +
                "values  (@methods, @total)";

            SqlParameter[] billparam = new SqlParameter[2];
            
            billparam[0] = new SqlParameter("@methods", Paymentmethods);
            billparam[1] = new SqlParameter("@total", Total);
            db.Database.ExecuteSqlCommand(query, billparam);
            Debug.Write(query);
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Carts.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Carts where CartID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Cart mycart = db.Carts.FromSql(query, param).FirstOrDefault();
            return View(mycart);
        }

        [HttpPost]
        public ActionResult Edit(int? id, int Total, string Paymentmethods)
        {
            if ((id == null) || (db.Carts.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Carts set Total=@total, Paymentmethods=@method " + " where CartID=@id";
            SqlParameter[] myparams = new SqlParameter[3];
            myparams[0] = new SqlParameter("@total", Total);
            myparams[1] = new SqlParameter("@method", Paymentmethods);
            myparams[2] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }
        //details


        public async Task<ActionResult> Details(int id)
        {
            if (db.Carts.Find(id) == null)
            {
                return NotFound();
            }

            Cart evnt = await db.Carts.SingleOrDefaultAsync(d => d.CartID == id);
            return View(evnt);
        }

        //delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cart donform = db.Carts.Find(id);
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
            Cart eve = await db.Carts.FindAsync(id);

            if (eve.CartID != id)
            {
                return Forbid();
            }

            db.Carts.Remove(eve);
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