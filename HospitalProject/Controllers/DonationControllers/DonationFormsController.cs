using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalProject.Data;
using HospitalProject.Models;

namespace HospitalProject.Controllers.DonationControllers
{
    public class DonationFormsController : Controller
    {
        private readonly HospitalCMSContext _context;

        public DonationFormsController(HospitalCMSContext context)
        {
            _context = context;
        }

        // GET: DonationForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonationForms.ToListAsync());
        }

        // GET: DonationForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationForm = await _context.DonationForms
                .SingleOrDefaultAsync(m => m.donationFormID == id);
            if (donationForm == null)
            {
                return NotFound();
            }

            return View(donationForm);
        }

        // GET: DonationForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonationForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("donationFormID,donationCause,charityName,donationGoal,presetAmounts,formDescription")] DonationForm donationForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donationForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donationForm);
        }

        // GET: DonationForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationForm = await _context.DonationForms.SingleOrDefaultAsync(m => m.donationFormID == id);
            if (donationForm == null)
            {
                return NotFound();
            }
            return View(donationForm);
        }

        // POST: DonationForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("donationFormID,donationCause,charityName,donationGoal,presetAmounts,formDescription")] DonationForm donationForm)
        {
            if (id != donationForm.donationFormID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationFormExists(donationForm.donationFormID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donationForm);
        }

        // GET: DonationForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationForm = await _context.DonationForms
                .SingleOrDefaultAsync(m => m.donationFormID == id);
            if (donationForm == null)
            {
                return NotFound();
            }

            return View(donationForm);
        }

        // POST: DonationForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donationForm = await _context.DonationForms.SingleOrDefaultAsync(m => m.donationFormID == id);
            _context.DonationForms.Remove(donationForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationFormExists(int id)
        {
            return _context.DonationForms.Any(e => e.donationFormID == id);
        }
    }
}
