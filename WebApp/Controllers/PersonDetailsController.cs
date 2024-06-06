using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonDetailsController : Controller
    {
        // GET: PersonDetails
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/personsDetails");
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonDetails> list = JsonConvert.DeserializeObject<List<PersonDetails>>(jstring);
                return View(list);
            }
            else
                return View(new List<PersonDetails>());
        }

        // GET: PersonDetails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
