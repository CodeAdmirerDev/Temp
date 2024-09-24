using System;

namespace CustomerDataMigrationProject.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsActive { get; set; } // Indicator if data is recent or legacy
    }
}
