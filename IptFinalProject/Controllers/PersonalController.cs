using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IptFinalProject.Data;
using IptFinalProject.Models;
using IptFinalProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace IptFinalProject.Controllers
{
    public class PersonalController : Controller
    {
        //private readonly ILogger<PersonalController> _logger;
        //private readonly UserManager<IptFinalProjectUser> _userManager;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IptFinalProjectContext _context;

        public PersonalController( /*UserManager<IptFinalProjectUser> userManager,*/ IptFinalProjectContext context)
        {
            //_logger = logger;
            //this._userManager = userManager;
            //_webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        
        // GET: Personal
        public async Task<IActionResult> Index()
        {
              return _context.PersonalInfo != null ? 
                          View(await _context.PersonalInfo.ToListAsync()) :
                          Problem("Entity set 'IptFinalProjectContext.PersonalViewModel'  is null.");
        }

        // GET: Personal/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                return NotFound();
            }

            var personalInfo = await _context.PersonalInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            return View(personalInfo);
        }

        // GET: Personal/Create
        public IActionResult Create()
        {
            //ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

        // POST: Personal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,PhoneNumber,Section,Course,YearLevel,DateOfBirth,Address,EmergencyContact")] PersonalInfoModel personalInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalInfo);
        }

        // GET: Personal/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                return NotFound();
            }

            var personalViewModel = await _context.PersonalInfo.FindAsync(id);
            if (personalViewModel == null)
            {
                return NotFound();
            }
            return View(personalViewModel);
        }

        // POST: Personal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,FirstName,LastName,PhoneNumber,Section,Course,YearLevel,DateOfBirth,Address,EmergencyContact")] PersonalInfoModel personalInfo)
        {
            if (id != personalInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalViewModelExists(personalInfo.Id))
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
            return View(personalInfo);
        }

        // GET: Personal/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                return NotFound();
            }

            var personalViewModel = await _context.PersonalInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalViewModel == null)
            {
                return NotFound();
            }

            return View(personalViewModel);
        }

        // POST: Personal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PersonalInfo == null)
            {
                return Problem("Entity set 'IptFinalProjectContext.PersonalViewModel'  is null.");
            }
            var personalViewModel = await _context.PersonalInfo.FindAsync(id);
            if (personalViewModel != null)
            {
                _context.PersonalInfo.Remove(personalViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalViewModelExists(string id)
        {
          return (_context.PersonalInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
