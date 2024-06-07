using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonDetailsController : Controller
    {

        private readonly IConfiguration _config;
        private readonly string _api;
        public PersonDetailsController(IConfiguration config)
        {
            _config = config;
            _api = _config.GetValue<string>("ApiSettings:ApiUrl");
        }
        // GET: PersonDetails
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(_api + "personsDetails");
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonDetails> list = JsonConvert.DeserializeObject<List<PersonDetails>>(jstring);
                return View(list);
            }
            else
                return View(new List<PersonDetails>());
        }


        public IActionResult Add()
        {
            PersonDetails personDetails = new PersonDetails();
            return View(personDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PersonDetails personDetails)
        {
            if (ModelState.IsValid)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };

                HttpClient client = new HttpClient();
                var jsonPerson = JsonConvert.SerializeObject(personDetails, settings);
                StringContent content = new StringContent(jsonPerson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync(_api + "personsDetails", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(personDetails);
                }

            }
            else
            {
                return View(personDetails);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(_api + "personsDetails/" + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                PersonDetails personDetails = JsonConvert.DeserializeObject<PersonDetails>(jstring);
                return View(personDetails);
            }
            else
                return RedirectToAction("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Update(PersonDetails personDetails)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonperson = JsonConvert.SerializeObject(personDetails);
                StringContent content = new StringContent(jsonperson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync(_api + "personsDetails", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(personDetails);
                }
            }
            else
                return View(personDetails);
        }
    }
}
