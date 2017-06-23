using ITCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITCompany.Controllers
{
    public class ClientController : Controller
    {
        private static ClientRepo clients;
        private List<Client> model = new List<Client>();

        public ClientController()
        {
            clients = new ClientRepo();
        }

        public ActionResult Index()
        {
            List<Client> model = clients.GetAll().ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID, Title, Contact")]Client client)       
        {
            if (ModelState.IsValid)
            {
                clients.Add(client);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            model = clients.GetAll().ToList();
            Client std = model.Where(s => s.ID == id).FirstOrDefault();
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
        public ActionResult Edit([Bind(Include = "ID, Title, Contact")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clients.Edit(client);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(client);
        }

        public ActionResult Delete(int id)
        {
            model = clients.GetAll().ToList();
            Client std = model.Where(s => s.ID == id).FirstOrDefault();
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
        public ActionResult DeleteConfirm(Client client)
        {
            try
            {
                clients.Delete(client);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(client);
        }
    }
}