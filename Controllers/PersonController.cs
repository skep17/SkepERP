using Microsoft.AspNetCore.Mvc;
using SkepERP.Interfaces;
using SkepERP.Models;

namespace SkepERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Person>))]
        public IActionResult GetPersons()
        {
            var persons = _personRepository.GetPersons();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(persons);
        }
    }
}
