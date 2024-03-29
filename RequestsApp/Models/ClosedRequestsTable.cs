using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class ClosedRequestsTable
    {
        public string Facility { get; set; } = null!;
        public string Employee { get; set; } = null!;
        public string Issue { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Phone { get; set; } = null!;
        public string? Comment { get; set; }
    }
}
