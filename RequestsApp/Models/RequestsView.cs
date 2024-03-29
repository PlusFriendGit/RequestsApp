using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class RequestsView
    {
        public string Name { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public int RequestId { get; set; }
        public string Issue { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool Status { get; set; }
        public string Phone { get; set; } = null!;
        public string? Comment { get; set; }
    }
}
