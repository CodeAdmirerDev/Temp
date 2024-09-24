using System;

namespace CustomerDataService.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public string Data { get; set; }
    }
}
