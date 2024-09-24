using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerDataMigrationProject.Models;

namespace CustomerDataMigrationProject.Services
{
    public class CustomerDataService
    {
        private readonly string _connectionString;

        public CustomerDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Customer>> GetActiveCustomersAsync()
        {
            var customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Customers WHERE IsActive = 1", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            LastModified = (DateTime)reader["LastModified"],
                            IsActive = (bool)reader["IsActive"]
                        });
                    }
                }
            }
            return customers;
        }

        // Other data access methods like AddCustomer, GetLegacyCustomers...
    }
}
