using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data;
using TimeSheet.Models;

namespace TimeSheet.Pages.Project
{
    public class EditModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public EditModel(TimeSheetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblProjects TblProjects { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToPage("/Login/Index");
            }

            TblProjects = await _context.TblProjects.FirstOrDefaultAsync(m => m.ProjectID == id);

            if (TblProjects == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TblProjects.ModifiedBy = HttpContext.Session.GetString("userid");
            TblProjects.ModifiedDate = DateTime.Now;

            _context.Attach(TblProjects).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProjectsExists(TblProjects.ProjectID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TblProjectsExists(int id)
        {
            return _context.TblProjects.Any(e => e.ProjectID == id);
        }
    }
}
