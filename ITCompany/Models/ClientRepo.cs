using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ITCompany.Models
{
    public class ClientRepo
    {
        static public string conStr;
        static ClientRepo()
        {
            conStr = ConfigurationManager.ConnectionStrings["ITCompanyDB"].ConnectionString;
        }
        public IEnumerable<Client> GetAll()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.client";
                con.Open();
                SqlDataReader client = cmd.ExecuteReader();
                while (client.Read())
                {
                    int id = (int)client["ID"];
                    string title = (string)client["Title"];
                    string contact = (string)client["Contact"];
                    Client newClient = new Client
                    {
                        ID = id,
                        Title = title,
                        Contact = contact,
                    };
                    yield return newClient;
                }
            }
        }

        public bool Add(Client client)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.client (Title, Contact) VALUES (@Title, @Contact)";
                cmd.Parameters.AddWithValue("@Title", client.Title);
                cmd.Parameters.AddWithValue("@Contact", client.Contact);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            throw new NotImplementedException();
        }

        public bool Edit(Client client)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.client SET " +
                    "Title = @Title, " +
                    "Contact = @Contact " +
                    "WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", client.ID);
                cmd.Parameters.AddWithValue("@Title", client.Title);
                cmd.Parameters.AddWithValue("@Contact", client.Contact);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(Client client)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Delete FROM dbo.client WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", client.ID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}