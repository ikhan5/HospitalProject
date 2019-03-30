using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalProject.Data;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class NavigationsController : Controller
    {
        private readonly HospitalCMSContext _context;

        public NavigationsController(HospitalCMSContext context)
        {
            _context = context;
        }

        // GET: Navigations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Navigations.ToListAsync());
        }

        // GET: Navigations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigation = await _context.Navigations
                .SingleOrDefaultAsync(m => m.navigationID == id);
            if (navigation == null)
            {
                return NotFound();
            }

            return View(navigation);
        }

        // GET: Navigations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Navigations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("navigationID,navigationName,navigationURL,navigationPosition")] Navigation navigation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navigation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navigation);
        }

        // GET: Navigations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigation = await _context.Navigations.SingleOrDefaultAsync(m => m.navigationID == id);
            if (navigation == null)
            {
                return NotFound();
            }
            return View(navigation);
        }

        // POST: Navigations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("navigationID,navigationName,navigationURL,navigationPosition")] Navigation navigation)
        {
            if (id != navigation.navigationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navigation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationExists(navigation.navigationID))
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
            return View(navigation);
        }

        // GET: Navigations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigation = await _context.Navigations
                .SingleOrDefaultAsync(m => m.navigationID == id);
            if (navigation == null)
            {
                return NotFound();
            }

            return View(navigation);
        }

        // POST: Navigations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var navigation = await _context.Navigations.SingleOrDefaultAsync(m => m.navigationID == id);
            _context.Navigations.Remove(navigation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationExists(int id)
        {
            return _context.Navigations.Any(e => e.navigationID == id);
        }
    }
}
