using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ITCompany.Models
{
    public class ProjectRepo
    {
        static public string conStr;
        static ProjectRepo()
        {
            conStr = ConfigurationManager.ConnectionStrings["ITCompanyDB"].ConnectionString;
        }
        public IEnumerable<Project> GetAll()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.project";
                con.Open();
                SqlDataReader project = cmd.ExecuteReader();
                while (project.Read())
                {
                    int id = (int)project["ID"];
                    int clientID = (int)project["ClientID"];
                    string status = (string)project["Status"];
                    string description = (string)project["Description"];
                    string title = (string)project["Title"];
                    decimal budget = (decimal)project["Budget"];    
                    Project newProject = new Project
                    {
                        ID = id,
                        ClientID = clientID,
                        Status = status,
                        Description = description,
                        Title = title,
                        Budget = budget,
                    };
                    yield return newProject;         
                }
            }
        }

        public bool Add(Project project)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.project (ClientID, Status, Description, Title, Budget) VALUES (@ClientID, @Status, @Description, @Title, @Budget)";
                cmd.Parameters.AddWithValue("@ClientID", project.ClientID);
                cmd.Parameters.AddWithValue("@Status", project.Status);
                cmd.Parameters.AddWithValue("@Description", project.Description);
                cmd.Parameters.AddWithValue("@Title", project.Title);
                cmd.Parameters.AddWithValue("@Budget", project.Budget);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            throw new NotImplementedException();
        }

        public bool Edit(Project project)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.project SET ClientID = @ClientID, Status = @Status, Description = @Description, Title = @Title, Budget = @Budget WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ClientID", project.ClientID);
                cmd.Parameters.AddWithValue("@Status", project.Status);
                cmd.Parameters.AddWithValue("@Description", project.Description);
                cmd.Parameters.AddWithValue("@Title", project.Title);
                cmd.Parameters.AddWithValue("@Budget", project.Budget);
                cmd.Parameters.AddWithValue("@ID", project.ID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(Project project)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Delete FROM dbo.project WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", project.ID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}