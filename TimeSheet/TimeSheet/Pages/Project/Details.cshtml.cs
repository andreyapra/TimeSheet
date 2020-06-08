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

namespace TimeSheet.Pages.Project
{
    public class DetailsModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public DetailsModel(TimeSheetContext context)
        {
            _context = context;
        }

        public TblProjects TblProjects { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblProjects = await _context.TblProjects.FirstOrDefaultAsync(m => m.ProjectID == id);

            if (TblProjects == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
