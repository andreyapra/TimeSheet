using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Models;

namespace TimeSheet.Data
{
    public class TimeSheetContext : DbContext
    {
        public TimeSheetContext (DbContextOptions<TimeSheetContext> options)
            : base(options)
        {
        }

        public DbSet<TimeSheet.Models.TblProjects> TblProjects { get; set; }

        public DbSet<TimeSheet.Models.TblTimeSheetEntry> TblTimeSheetEntry { get; set; }
    }
}
