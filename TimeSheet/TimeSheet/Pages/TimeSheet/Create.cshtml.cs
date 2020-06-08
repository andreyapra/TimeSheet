using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeSheet.Data;
using TimeSheet.Models;

namespace TimeSheet.Pages.TimeSheet
{
    public class CreateModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public CreateModel(TimeSheetContext context)
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


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToPage("/Login/Index");
            }

            PopulateProject();

            return Page();
        }

        [BindProperty]
        public TblTimeSheetEntry TblTimeSheetEntry { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TblTimeSheetEntry.UserID = HttpContext.Session.GetString("userid");
            TblTimeSheetEntry.Status = "SUBMIT";
            TblTimeSheetEntry.ManagerID = HttpContext.Session.GetString("managerid");
            _context.TblTimeSheetEntry.Add(TblTimeSheetEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
