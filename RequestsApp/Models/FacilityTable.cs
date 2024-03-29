using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class FacilityTable
    {
        public FacilityTable()
        {
            DocumentsTables = new HashSet<DocumentsTable>();
            RequestsTables = new HashSet<RequestsTable>();
        }

        public int FacilityId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string DirName { get; set; } = null!;
        public string AgentName { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<DocumentsTable> DocumentsTables { get; set; }
        public virtual ICollection<RequestsTable> RequestsTables { get; set; }
    }
}
