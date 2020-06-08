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

namespace TimeSheet.Pages.TimeSheet
{
    public class EditModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public EditModel(TimeSheetContext context)
        {
            _context = context;
        }

        public SelectList ProjectSL { get; set; }

        public void PopulateProject()
        {
            var CatQuery = from d in _context.TblProjects
                           where d.IsActive == true
                           orderby d.ProjectID
                           select d;

            ProjectSL = new SelectList(CatQuery, "ProjectID", "ProjectName");
        }

        [BindProperty]
        public TblTimeSheetEntry TblTimeSheetEntry { get; set; }

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

            PopulateProject();

            TblTimeSheetEntry = await _context.TblTimeSheetEntry.FirstOrDefaultAsync(m => m.TimesheetID == id);

            if (TblTimeSheetEntry == null)
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

            TblTimeSheetEntry.Status = "SUBMIT";
            _context.Attach(TblTimeSheetEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblTimeSheetEntryExists(TblTimeSheetEntry.TimesheetID))
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

        private bool TblTimeSheetEntryExists(int id)
        {
            return _context.TblTimeSheetEntry.Any(e => e.TimesheetID == id);
        }
    }
}
