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


        public void AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                string query = "INSERT INTO Employees (Name, Email, Department, Salary) " +
                               "VALUES (@Name, @Email, @Department, @Salary)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Department", (object?)employee.Department ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public Employee? GetEmployeeId(int id)
        {
            Employee? employee = null;
            using SqlConnection connection = new(_connectionString);

            string query = "SELECT Id, Name, Email, Department, Salary " +
                    "FROM Employees " +
                    "WHERE Id = @Id";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = new Employee
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    Department = reader.IsDBNull("Department")
                        ? null
                        : reader.GetString("Department"),
                    Salary = reader.GetDecimal("Salary")
                };
            }

            return employee;
        }




    }
}
