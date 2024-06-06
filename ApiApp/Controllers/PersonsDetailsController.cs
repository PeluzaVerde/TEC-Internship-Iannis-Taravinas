using ApiApp.Model;
using Internship.Model;
using Microsoft.AspNetCore.Mvc;

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
    }
}
