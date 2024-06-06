﻿using WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        //HINT task 8 start

/*        private readonly IConfiguration _config;
        private readonly string _api;
        public PersonController(IConfiguration config)
        {
            _config = config;
            _api = _config.GetValue<string>("");
        }*/

        //HINT task 8 end
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/persons");
            if(message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonInformation> list = JsonConvert.DeserializeObject<List<PersonInformation>>(jstring);
                return View(list);
            }
            else
            return View(new List<PersonInformation>());
        }
        public IActionResult Add()
        {
            Person person = new Person();
            return View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            if(ModelState.IsValid)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                HttpClient client = new HttpClient();
                var jsonPerson = JsonConvert.SerializeObject(person,settings);
                StringContent content = new StringContent(jsonPerson,Encoding.UTF8,"application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:5229/api/persons", content);
                if(message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(person);
                }

            }
            else
            {
                return View(person);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/persons/" + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Person person = JsonConvert.DeserializeObject<Person>(jstring);
                return View(person);
            }
            else
                return RedirectToAction("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonperson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonperson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync("http://localhost:5229/api/persons", content);
                if(message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(person);
                }
            }
            else
                return View(person);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5229/api/persons/{id}");
            if (!response.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index");
            }

            // Deserialize the person from the response
            var personJson = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(personJson);

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync($"http://localhost:5229/api/persons/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "There is an API Error");
                return RedirectToAction("Index");
            }
        }
    }
            
}
