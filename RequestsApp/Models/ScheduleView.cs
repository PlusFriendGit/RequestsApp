using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class ScheduleView
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public string Issue { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public bool Status { get; set; }
        public string Phone { get; set; } = null!;
        public string? Comment { get; set; }
    }
}
