using System;
using System.Collections.Generic;

namespace RequestsApp.Models
{
    public partial class EmployeesTable
    {
        public EmployeesTable()
        {
            RequestsTables = new HashSet<RequestsTable>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string ThirdName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string Position { get; set; } = null!;

        public virtual ICollection<RequestsTable> RequestsTables { get; set; }
    }
}
