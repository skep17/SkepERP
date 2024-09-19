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

        [HttpGet("GetPersonById/{id}")]
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

        [HttpPut("UpdatePerson/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult UpdatePerson(int id, [FromBody] UpdatePersonDto person)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.UpdatePerson(id, person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("AddPhones/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult AddPhones(int id, UpdatePhoneDto phones)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.AddPhones(id, phones);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete("RemovePhones/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult RemovePhones(int id, UpdatePhoneDto phones)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.RemovePhones(id, phones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("AddRelations/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult AddRelations(int id, UpdateRelationsDto relations)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.AddRelations(id, relations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete("RemoveRelations/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public IActionResult RemoveRelations(int id, UpdateRelationsDto relations)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.RemoveRelations(id, relations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete("DeletePerson/{id}")]
        [ProducesResponseType(200)]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                _personRepository.DeletePerson(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok($"Person with id {id} deleted.");
        }

    }
}
