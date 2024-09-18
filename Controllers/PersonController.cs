using Microsoft.AspNetCore.Mvc;
using SkepERP.Dto;
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

        [HttpGet("GetPersons")]
        [ProducesResponseType(200, Type = typeof(ICollection<PersonDto>))]
        public IActionResult GetPersons()
        {
            var persons = _personRepository.GetPersons();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(persons);
        }

        [HttpGet("GetPersonById")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult GetPersonById(int id)
        {
            var person = _personRepository.GetPersonById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(person);
        }

        [HttpPost("CreatePerson")]
        [ProducesResponseType(201, Type = typeof(PersonDto))]
        public IActionResult CreatePerson(CreatePersonDto person)
        {
            PersonDto? result;
            
            try
            {
                result = _personRepository.CreatePerson(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetPersonById), new { id = result?.Id }, result);
        }
    }
}
