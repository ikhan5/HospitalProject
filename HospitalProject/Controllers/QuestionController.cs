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
    public class QuestionController : Controller
    {
        private readonly HospitalCMSContext db;

        public QuestionController(HospitalCMSContext context)
        {
            db = context;
        }

      /*  public async Task<IActionResult> Index()
        {
            return View(await db.Questions.ToListAsync());
        }*/

        public async Task<ActionResult> Index(int pagenum)
        {
            // Pagination
       

            var _questions = await db.Questions.ToListAsync();
            int questionCount = _questions.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)questionCount / perpage) - 1;
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

            List<Question> questions = await db.Questions.Skip(start).Take(perpage).ToListAsync();
            return View(questions);
        }
        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("QuestionList, AnswerList")] Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(question);
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
            return View(question);
        }

        //edit
        public ActionResult Edit(int? id)
        {
            if ((id == null) || (db.Questions.Find(id) == null))
            {
                return NotFound();
            }
            string query = "select * from Questions where QuestionID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Question myquestion = db.Questions.FromSql(query, param).FirstOrDefault();
            return View(myquestion);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string QuestionList, string AnswerList)
        {
            if ((id == null) || (db.Questions.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update Questions set QuestionList=@questionlist, AnswerList=@answerlist" +
                " where QuestionID=@id";
            SqlParameter[] myparams = new SqlParameter[3];
            myparams[0] = new SqlParameter("@questionlist", QuestionList);
            myparams[1] = new SqlParameter("@answerlist", AnswerList);
            myparams[2] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

        //details
        public async Task<ActionResult> Details(int id)
        {
            if (db.Questions.Find(id) == null)
            {
                return NotFound();
            }

            Question question = await db.Questions.SingleOrDefaultAsync(d => d.QuestionID == id);
            return View(question);
        }

        //delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question donform = db.Questions.Find(id);
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
            Question question = await db.Questions.FindAsync(id);

            if (question.QuestionID != id)
            {
                return Forbid();
            }

            db.Questions.Remove(question);
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