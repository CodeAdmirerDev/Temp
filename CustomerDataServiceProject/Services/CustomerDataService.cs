using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerDataService.Models;

namespace CustomerDataService.Services
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
                var command = new SqlCommand("SELECT * FROM Customers WHERE LastModified > '2020-01-01'", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            LastModified = (DateTime)reader["LastModified"],
                            Data = reader["Data"].ToString()
                        });
                    }
                }
            }
            return customers;
        }

        public async Task SaveLegacyCustomerToBlobAsync(Customer customer)
        {
            // Implement logic to save customer data to Azure Blob
        }
    }
}
