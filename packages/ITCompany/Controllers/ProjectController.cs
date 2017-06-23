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
    public class ProjectController : Controller
    {
        private static ProjectRepo projects;
        private List<Project> model = new List<Project>();

        public ProjectController()
        {        
            projects = new ProjectRepo();            
        }

        public ActionResult Index()
        {
            List<Project> model = projects.GetAll().ToList();
            return View(model);
        }

        public ActionResult Create()
        {
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