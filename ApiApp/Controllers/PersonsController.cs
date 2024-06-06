using ApiApp.Model;
using Internship.Model;
using Internship.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new APIDbContext();
            var list = db.Persons.Include(x => x.Salary).Include(x => x.Position)
                   .Select(x => new PersonInformation()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       PositionName = x.Position.Name,
                       Salary = x.Salary.Amount,
                   }).ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var db = new APIDbContext();
            Person person = db.Persons.FirstOrDefault(x => x.Id == Id);
            if (person == null)
                return NotFound();
            else
                return Ok(person);

        }
        [HttpPost]
        public IActionResult Post(PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    Name = personViewModel.Name,
                    Surname = personViewModel.Surname,
                    Age = personViewModel.Age,
                    Email = personViewModel.Email,
                    Address = personViewModel.Address,
                    PositionId = personViewModel.PositionId,
                    SalaryId = personViewModel.SalaryId
                };

                var db = new APIDbContext();
                person.Position = db.Positions.Find(person.PositionId);
                person.Salary = db.Salaries.Find(person.SalaryId);
                if (person.Position == null || person.Salary == null)
                {
                    return BadRequest("Invalid PositionId or SalaryId");
                }
                db.Persons.Add(person);
                db.SaveChanges();
                return Created("", person);
            }
            else
                return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdatePerson(PersonViewModel person)
        {

            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                Person updateperson = db.Persons.Find(person.Id);
                updateperson.Address = person.Address;
                updateperson.Age = person.Age;
                updateperson.Email = person.Email;
                updateperson.Name = person.Name;
                updateperson.PositionId = person.PositionId;
                updateperson.SalaryId = person.SalaryId;
                updateperson.Position = db.Positions.Find(person.PositionId);
                updateperson.Salary = db.Salaries.Find(person.SalaryId);
                updateperson.Surname = person.Surname;

                if(updateperson.Salary == null || updateperson.Position == null)
                {
                    return BadRequest();
                }
                db.SaveChanges();
                return NoContent();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var db = new APIDbContext();
            var person = await db.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.Persons.Remove(person);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
