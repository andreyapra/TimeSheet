using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TimeSheet.Models
{
    public class TblProjects
    {
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }

        public ICollection<TblTimeSheetEntry> TblTimeSheets { get; set; }

    }

    public class TblTimeSheetEntry
    {
        [Key]
        public int TimesheetID { get; set; }
        public string UserID { get; set; }
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public int ProjectID { get; set; }
        public int WorkHour { get; set; }
        public string Keterangan { get; set; }
        public string Status { get; set; }
        public string ManagerID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ApproveDate { get; set; }

        public TblProjects TblProjects { get; set; }

    }

}
