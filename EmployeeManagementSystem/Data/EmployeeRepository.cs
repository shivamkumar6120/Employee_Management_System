using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;   
using EmployeeManagementSystem.Models;


namespace EmployeeManagementSystem.Data
{
    public class EmployeeRepository
    {

        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration) // IConfiguration - Reads appsettings.json
        {
            _connectionString = configuration.GetConnectionString("EmployeeDBConnection")
                ?? throw new InvalidOperationException("EmployeeDBConnection is not configured.");

            //_connectionString - DB connection 
        }

      

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_connectionString))// SqlConnection  - Connect to SQL Server and using is like try with resources in java
            {
                string query = "SELECT Id, Name, Email, Department, Salary FROM Employees";
                SqlCommand command = new SqlCommand(query, connection); // SqlCommand - Execute SQL
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();//  SqlDataReader - Read rows one by one
                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Email = reader.GetString("Email"),
                        Department = reader.IsDBNull("Department")
                            ? null
                            : reader.GetString("Department"),
                        Salary = reader.GetDecimal("Salary")

                    };
                    employees.Add(emp);
                }
            }
            return employees;

        }
    }
}
