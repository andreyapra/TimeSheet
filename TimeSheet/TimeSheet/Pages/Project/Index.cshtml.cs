using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data;
using TimeSheet.Models;

namespace TimeSheet.Pages.Project
{
    public class IndexModel : PageModel
    {
        private readonly TimeSheetContext _context;

        public IndexModel(TimeSheetContext context)
        {
            _context = context;
        }

        public IList<TblProjects> TblProjects { get;set; }

        public async Task OnGetAsync()
        {
            TblProjects = await _context.TblProjects.Where(t => t.IsActive == true).ToListAsync();
        }
    }
}
