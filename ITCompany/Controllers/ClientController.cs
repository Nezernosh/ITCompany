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
            return View("Save", new Client());
        }

        public ActionResult Edit(int id)
        {
            return View("Save", clients.GetAll().ToList().Where(s => s.ID == id).FirstOrDefault());
        }
              
        [HttpPost]
        public ActionResult Save([Bind(Include = "ID, Title, Contact")] Client client)
        {         
            try
            {
                if (ModelState.IsValid)
                {
                    if (client.ID <= 0)
                    {
                        clients.Add(client);                    
                    }
                    else
                    {
                        clients.Edit(client);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View("Save", client);
        }

        public ActionResult Delete(int id)
        {
            model = clients.GetAll().ToList();
            Client std = model.Where(s => s.ID == id).FirstOrDefault();

            if (std == null) return HttpNotFound();

            try
            {
                clients.Delete(std);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
                return View(std);
            }
        }

    }
}