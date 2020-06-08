using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data;
using TimeSheet.Models;

namespace TimeSheet.Pages.TimeSheet
{
    public class DetailsModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public DetailsModel(TimeSheetContext context)
        {
            _context = context;
        }

        public TblTimeSheetEntry TblTimeSheetEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblTimeSheetEntry = await _context.TblTimeSheetEntry.FirstOrDefaultAsync(m => m.TimesheetID == id);

            if (TblTimeSheetEntry == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
