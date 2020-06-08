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
    public class IndexModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public IndexModel(TimeSheetContext context)
        {
            _context = context;
        }

        public IList<TblTimeSheetEntry> TblTimeSheetEntry { get;set; }
        public string SessionUser { get; set; }

        public string GetProject(int id)
        {
            string Project;
            Project = _context.TblProjects.Where(m => m.ProjectID == id).FirstOrDefault().ProjectName;

            return Project;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToPage("/Login/Index");
            }

            SessionUser = HttpContext.Session.GetString("userid");

            TblTimeSheetEntry = await _context.TblTimeSheetEntry.Where(s => s.Status == "SUBMIT").Where(t => t.ManagerID == SessionUser).ToListAsync();

            return Page();
        }
    }
}
