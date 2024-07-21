using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bader.Models;

namespace Bader.Controllers
{
    public class UsersController : Controller
    {
        private readonly BaaderContext _context;

        public UsersController(BaaderContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return View(await _context.tblUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tblUsers == null)
            {
                return NotFound();
            }

            var tblUsers = await _context.tblUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblUsers == null)
            {
                return NotFound();
            }

            return View(tblUsers);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,FullNameAr,FullNameEn,Password,Email,Phone,Usertype")] tblUsers tblUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblUsers);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tblUsers == null)
            {
                return NotFound();
            }

            var tblUsers = await _context.tblUsers.FindAsync(id);
            if (tblUsers == null)
            {
                return NotFound();
            }
            return View(tblUsers);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,FullNameAr,FullNameEn,Password,Email,Phone,Usertype")] tblUsers tblUsers)
        {
            if (id != tblUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblUsersExists(tblUsers.Id))
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
            return View(tblUsers);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tblUsers == null)
            {
                return NotFound();
            }

            var tblUsers = await _context.tblUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblUsers == null)
            {
                return NotFound();
            }

            return View(tblUsers);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tblUsers == null)
            {
                return Problem("Entity set 'BaaderContext.tblUsers'  is null.");
            }
            var tblUsers = await _context.tblUsers.FindAsync(id);
            if (tblUsers != null)
            {
                _context.tblUsers.Remove(tblUsers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblUsersExists(int id)
        {
          return _context.tblUsers.Any(e => e.Id == id);
        }
    }
}
