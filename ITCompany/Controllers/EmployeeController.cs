using ITCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITCompany.Controllers
{
    public class EmployeeController : Controller
    {
        private static EmployeeRepo employees;
        private List<Employee> model = new List<Employee>();

        public EmployeeController()
        {
            employees = new EmployeeRepo();
        }

        public ActionResult Index()
        {
            List<Employee> model = employees.GetAll().ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View("Save", new Employee());
        }      

        public ActionResult Edit(int id)
        {
            return View("Save", employees.GetAll().ToList().Where(s => s.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Save([Bind(Include = "ID, FullName, Phone, Email, Position")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee.ID <= 0)
                    {
                        employees.Add(employee);
                    }
                    else
                    {
                        employees.Edit(employee);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View("Save", employee);
        }
       
        public ActionResult Delete(int id)
        {
            model = employees.GetAll().ToList();
            Employee std = model.Where(s => s.ID == id).FirstOrDefault();
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
        public ActionResult DeleteConfirm(Employee employee)
        {
            try
            {
                employees.Delete(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(employee);
        }
    }
}