using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class RequestsTable
    {
        public int RequestId { get; set; }
        public int FacilityId { get; set; }
        public int EmployeeId { get; set; }
        public string Issue { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool Status { get; set; }
        public string Phone { get; set; } = null!;
        public string? Comment { get; set; }

        public virtual EmployeesTable Employee { get; set; } = null!;
        public virtual FacilityTable Facility { get; set; } = null!;
    }
}
