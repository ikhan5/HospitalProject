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
    public class ItemController : Controller
    {
        private readonly HospitalCMSContext db;

    public ItemController(HospitalCMSContext context)
    {
        db = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await db.Items.Include(d => d.cart).ToListAsync());
    }


    public ActionResult Create()
    {
            CartList cl = new CartList();
            cl.carts = db.Carts.ToList();
            return View(cl);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string ItemDescp, string Name, int price, int quantity, int CartID)
        {
            string query = "insert into Items (ItemDescp, Name, price, quantity, CartID )" +
                "values  (@descp, @name, @price, @quantity, @id)";

            SqlParameter[] billparam = new SqlParameter[5];

            billparam[0] = new SqlParameter("@descp", ItemDescp);
            billparam[1] = new SqlParameter("@name", Name);
            billparam[2] = new SqlParameter("@price", price);
            billparam[3] = new SqlParameter("@quantity", quantity);
            billparam[4] = new SqlParameter("@id", CartID);
            db.Database.ExecuteSqlCommand(query, billparam);
            Debug.Write(query);
            return RedirectToAction("Index");

        }
        public async Task<ActionResult> Edit(int id)
        {
            
            CartList dfl = new CartList();
            dfl.item = db.Items.Include(d => d.cart)
                           .SingleOrDefault(d => d.ItemID == id);
            dfl.carts = db.Carts.ToList();
            if (dfl != null) return View(dfl);
            else return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string ItemDescp, string Name, int price, int quantity, int CartID)
        {
            if (db.Items.Find(CartID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Items set ItemDescp=@descp, Name=@name, price = @price, quantity =@quantity, CartID=@id " +
                " where CartID=@id AND ItemID=@formID";
            SqlParameter[] donparams = new SqlParameter[5];
            donparams[0] = new SqlParameter("@descp", ItemDescp);
            donparams[1] = new SqlParameter("@name", Name);
            donparams[2] = new SqlParameter("@price", price);
            donparams[3] = new SqlParameter("@quantity", quantity);
            donparams[4] = new SqlParameter("@id", CartID);
            


            db.Database.ExecuteSqlCommand(updateQuery, donparams);
            return RedirectToAction("Details/" + CartID);
        }
    }
}