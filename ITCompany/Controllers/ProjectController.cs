using ITCompany.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITCompany.Controllers
{
    public class Pair
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }

    public class ProjectController : Controller
    {
        private static ProjectRepo projects;
        private List<Project> model = new List<Project>();
        private List<Pair> titles = new List<Pair>();

        public ProjectController()
        {
            projects = new ProjectRepo();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ITCompanyDB"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.client";
                con.Open();
                SqlDataReader project = cmd.ExecuteReader();
                while (project.Read())
                {
                    int id = (int)project["ID"];
                    string title = (string)project["Title"];
                    Pair tempTitle = new Pair
                    {
                        ID = id,                     
                        Title = title,
                    };
                    titles.Add(tempTitle);
                }
            }
        }

        public ActionResult Index()
        {
            List<Project> model = projects.GetAll().ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(titles, "ID", "Title");
            return View("Save", new Project());
        }     

        public ActionResult Edit(int id)
        {
            ViewBag.ClientID = new SelectList(titles, "ID", "Title");
            return View("Save", projects.GetAll().ToList().Where(s => s.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Save([Bind(Include = "ID, ClientID, Status, Description, Title, Budget")] Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (project.ID <= 0)
                    {
                        projects.Add(project);
                    }
                    else
                    {
                        projects.Edit(project);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            ViewBag.ClientID = new SelectList(titles, "ID", "Title");
            return View("Save", project);
        }

        public ActionResult Delete(int id)
        {
            model = projects.GetAll().ToList();
            Project std = model.Where(s => s.ID == id).FirstOrDefault();

            if (std == null) return HttpNotFound();

            try
            {
                projects.Delete(std);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }
            return View("Index", std);
        }
    }
}