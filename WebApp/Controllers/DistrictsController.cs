﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace CeylonFoot.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly WebAppDbContext _context;

        public DistrictsController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: Districts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Districts.ToListAsync());
        }

        // GET: Districts/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            var district = await _context.Districts.Where(d => d.DistrictId == id)
                .Include(d => d.Posts)
                .ThenInclude(p => p.Category).FirstOrDefaultAsync();
            //   .ThenInclude(c => c.CategoryId == 2).FirstOrDefaultAsync();

            //  .AsNoTracking();


            return View(district);
        }

        // GET: Districts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Districts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistrictId,DistrictName")] District district)
        {
            if (ModelState.IsValid)
            {
                _context.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(district);
        }

        // GET: Districts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            return View(district);
        }

        // POST: Districts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DistrictId,DistrictName")] District district)
        {
            if (id != district.DistrictId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictExists(district.DistrictId))
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
            return View(district);
        }

        // GET: Districts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _context.Districts
                .FirstOrDefaultAsync(m => m.DistrictId == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictExists(int id)
        {
            return _context.Districts.Any(e => e.DistrictId == id);
        }
    }
}
