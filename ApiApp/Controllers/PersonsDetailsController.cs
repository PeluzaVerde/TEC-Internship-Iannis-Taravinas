using ApiApp.Model;
using Internship.Model;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsDetailsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new APIDbContext();
            var list = db.PersonDetails.ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var db = new APIDbContext();
            PersonDetails personDetails = db.PersonDetails.Find(Id);
            if (personDetails == null)
                return NotFound();
            else
                return Ok(personDetails);
        }

        [HttpPost]
        public IActionResult Post(PersonDetailsViewModel personDetailsViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var personDetails = new PersonDetails()
                    {
                        BirthDay = personDetailsViewModel.BirthDay,
                        PersonCity = personDetailsViewModel.PersonCity,
                        PersonId = personDetailsViewModel.PersonId,
                    };
                    var db = new APIDbContext();
                    personDetails.Person = db.Persons.Find(personDetails.PersonId);
                    if (personDetails.Person == null)
                    {
                        return BadRequest("Invalid PersonId");
                    }
                    db.PersonDetails.Add(personDetails);
                    db.SaveChanges();
                    return Created();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdatePersonsDetails(PersonDetailsViewModel personDetails)
        {

            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                PersonDetails updatepersonDetails = db.PersonDetails.Find(personDetails.Id);
                updatepersonDetails.PersonCity = personDetails.PersonCity;
                updatepersonDetails.BirthDay = personDetails.BirthDay;
                updatepersonDetails.PersonId = personDetails.PersonId;
                updatepersonDetails.Person = db.Persons.Find(personDetails.PersonId);

                if (updatepersonDetails.Person == null)
                {
                    return BadRequest();
                }
                db.SaveChanges();
                return NoContent();
            }
            else
                return BadRequest();
        }

    }


}
