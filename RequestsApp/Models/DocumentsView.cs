using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class DocumentsView
    {
        public int DocumentId { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
