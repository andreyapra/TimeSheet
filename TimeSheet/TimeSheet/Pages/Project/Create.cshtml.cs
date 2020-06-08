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

namespace TimeSheet.Pages.Project
{
    public class CreateModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public CreateModel(TimeSheetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToPage("/Login/Index");
            }

                return Page();
        }

        [BindProperty]
        public TblProjects TblProjects { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TblProjects.IsActive = true;
            TblProjects.CreatedBy = HttpContext.Session.GetString("userid");
            TblProjects.CreatedDate = DateTime.Now;
            TblProjects.ModifiedBy = HttpContext.Session.GetString("userid");
            TblProjects.ModifiedDate = DateTime.Now;

            _context.TblProjects.Add(TblProjects);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
