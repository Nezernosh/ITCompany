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
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID, ClientID, Status, Description, Title, Budget")]Project project)
        {
            if (ModelState.IsValid)
            {
                projects.Add(project);
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(titles, "ID", "Title");
            return View();
        }

        public ActionResult Edit(int id)
        {
            model = projects.GetAll().ToList();
            Project std = model.Where(s => s.ID == id).FirstOrDefault();
            if (std == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(std);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID, ClientID, Status, Description, Title, Budget")] Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    projects.Edit(project);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(project);
        }

        public ActionResult Delete(int id)
        {
            model = projects.GetAll().ToList();
            Project std = model.Where(s => s.ID == id).FirstOrDefault();
            if (std == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(std);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(Project project)
        {
            try
            {
                projects.Delete(project);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(project);
        }
    }
}