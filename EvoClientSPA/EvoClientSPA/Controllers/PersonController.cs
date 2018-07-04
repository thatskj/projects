using EvoClientSPA.APIServices;
using EvoClientSPA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;

namespace EvoClientSPA.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            IList<PersonVM> listPersons = new List<PersonVM>();
            HttpResponseMessage response = PersonClientAPI.GetResponseFromAPI();
            if (response.IsSuccessStatusCode)
            {
                var persons = response.Content.ReadAsAsync<IEnumerable<PersonVM>>().Result;
                listPersons = persons.ToList();
            }
            return View(listPersons);
        }

        public ActionResult Details(int id)
        {
            PersonVM person = new PersonVM();
            HttpResponseMessage response = PersonClientAPI.GetResponseFromAPI(id.ToString());
            if (response.IsSuccessStatusCode)
            {
                person = response.Content.ReadAsAsync<PersonVM>().Result;
            }
            return View(person);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create

        public async Task<ActionResult> Create(PersonVM model)
        {
            try
            {
                await PersonClientAPI.PostToAPI(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PersonVM person = new PersonVM();
            HttpResponseMessage response = PersonClientAPI.GetResponseFromAPI(id.ToString());
            if (response.IsSuccessStatusCode)
            {
                person = response.Content.ReadAsAsync<PersonVM>().Result;
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(int Id, PersonVM model)
        {
            try
            {
                PersonClientAPI.EditPerson(model.Id, model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            PersonVM person = new PersonVM();
            HttpResponseMessage response = PersonClientAPI.GetResponseFromAPI(id.ToString());
            if (response.IsSuccessStatusCode)
            {
                person = response.Content.ReadAsAsync<PersonVM>().Result;
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(int id, PersonVM person)
        {
            try
            {
                var response = PersonClientAPI.DeleteToAPI(id.ToString(), person);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
