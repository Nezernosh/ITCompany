using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ITCompany.Models
{
    public class EmployeeRepo
    {
        static public string conStr;
        static EmployeeRepo()
        {
            conStr = ConfigurationManager.ConnectionStrings["ITCompanyDB"].ConnectionString;
        }
        public IEnumerable<Employee> GetAll()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.employee";
                con.Open();
                SqlDataReader employee = cmd.ExecuteReader();
                while (employee.Read())
                {
                    int id = (int)employee["ID"];
                    string fullName = (string)employee["FullName"];
                    string phone = (string)employee["Phone"];
                    string email = (string)employee["Email"];
                    string position = (string)employee["Position"];
                    Employee newEmployee = new Employee
                    {
                        ID = id,
                        FullName = fullName,
                        Phone =phone,
                        Email = email, 
                        Position = position,
                    };
                    yield return newEmployee;
                }
            }
        }

        public bool Add(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.employee (FullName, Phone, Email, Position) VALUES (@FullName, @Phone, @Email, @Position)";
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            throw new NotImplementedException();
        }

        public bool Edit(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.employee SET FullName = @FullName, Phone = @Phone, Email = @Email, Position = @Position WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@ID", employee.ID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Delete FROM dbo.employee WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", employee.ID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}