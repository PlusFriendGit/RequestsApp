using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class DocumentsTable
    {
        public int DocumentId { get; set; }
        public int FacilityId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual FacilityTable Facility { get; set; } = null!;
    }
}
