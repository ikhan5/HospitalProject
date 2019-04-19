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
            //find job posting form where 
            ItemList cl = new ItemList();
            cl.carts = db.Carts.Include(d => d.Items)
                           .SingleOrDefault(d => d.CartID == id);
            cl.item = db.Items.ToList();


            if (cl != null) return View(cl);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string ItemDescp, string Name, int price, int quantity, int CartID)
        {
            if (db.Items.Find(CartID) == null)
            {
                return NotFound();
            }

            string updateQuery = "update Items set ItemDescp=@descp, Name=@name,price=@price, quantity= @quantity" +" where ItemID=@id";
            SqlParameter[] postparams = new SqlParameter[5];
            postparams[0] = new SqlParameter("@id", CartID);
            postparams[1] = new SqlParameter("@descp", ItemDescp);
            postparams[2] = new SqlParameter("@name", Name);
            postparams[3] = new SqlParameter("@price", price);
            postparams[4] = new SqlParameter("@quantity", quantity);
            

            db.Database.ExecuteSqlCommand(updateQuery, postparams);
            return RedirectToAction("Details/" + CartID);
        }

    }
}