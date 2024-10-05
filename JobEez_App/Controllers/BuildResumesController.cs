using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobEez_App.Models;

namespace JobEez_App.Controllers
{
    public class BuildResumesController : Controller
    {
        private readonly JobEezPrimeTechContext _context;

        public BuildResumesController(JobEezPrimeTechContext context)
        {
            _context = context;
        }

        // GET: BuildResumes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BuildResumes.ToListAsync());
        }

        // GET: BuildResumes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildResume = await _context.BuildResumes
                .FirstOrDefaultAsync(m => m.PersonalInfoId == id);
            if (buildResume == null)
            {
                return NotFound();
            }

            return View(buildResume);
        }

        // GET: BuildResumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BuildResumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,Company,WorkLocation,WorkerStartDate,WorkerEndDate,Responsibilities,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildResume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = buildResume.PersonalInfoId });
            }
            return View(buildResume);
        }

        // GET: BuildResumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildResume = await _context.BuildResumes.FindAsync(id);
            if (buildResume == null)
            {
                return NotFound();
            }
            return View(buildResume);
        }

        // POST: BuildResumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,Company,WorkLocation,WorkerStartDate,WorkerEndDate,Responsibilities,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
        {
            if (id != buildResume.PersonalInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildResume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildResumeExists(buildResume.PersonalInfoId))
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
            return View(buildResume);
        }

        // GET: BuildResumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildResume = await _context.BuildResumes
                .FirstOrDefaultAsync(m => m.PersonalInfoId == id);
            if (buildResume == null)
            {
                return NotFound();
            }

            return View(buildResume);
        }

        // POST: BuildResumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildResume = await _context.BuildResumes.FindAsync(id);
            if (buildResume != null)
            {
                _context.BuildResumes.Remove(buildResume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildResumeExists(int id)
        {
            return _context.BuildResumes.Any(e => e.PersonalInfoId == id);
        }

        // GET: BuildResumes/Register
        public IActionResult Register()
        {
            return View(); // Create a view named Register.cshtml in the corresponding Views/BuildResumes folder
        }

        // POST: BuildResumes/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,Company,WorkLocation,WorkerStartDate,WorkerEndDate,Responsibilities,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildResume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to the Index action after successful registration
            }
            return View(buildResume);
        }
    }
}
