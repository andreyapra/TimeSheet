using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data;
using TimeSheet.Models;

namespace TimeSheet.Pages.Approval
{
    public class ApproveModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public ApproveModel(TimeSheetContext context)
        {
            _context = context;
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

            TblTimeSheetEntry = await _context.TblTimeSheetEntry.FirstOrDefaultAsync(m => m.TimesheetID == id);

            if (TblTimeSheetEntry == null)
            {
                return NotFound();
            }
            else
            {
                TblTimeSheetEntry.Status = "APPROVE";
                TblTimeSheetEntry.ApproveDate = DateTime.Now;
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
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblTimeSheetEntry = await _context.TblTimeSheetEntry.FindAsync(id);

            //if (TblTimeSheetEntry != null)
            //{
            //    _context.TblTimeSheetEntry.Remove(TblTimeSheetEntry);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }

        private bool TblTimeSheetEntryExists(int id)
        {
            return _context.TblTimeSheetEntry.Any(e => e.TimesheetID == id);
        }

    }
}
