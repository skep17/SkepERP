using Microsoft.AspNetCore.Mvc;
using SkepERP.Dto;
using SkepERP.Interfaces;
using SkepERP.Models;
using System;
using System.Text;

namespace SkepERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IErrorLogRepository _logRepository;

        public PersonController(IPersonRepository personRepository, IErrorLogRepository logRepository)
        {
            this._personRepository = personRepository;
            this._logRepository = logRepository;
        }

        [HttpGet("GetPersons/All")]
        [ProducesResponseType(200, Type = typeof(ICollection<PersonDto>))]
        public async Task<IActionResult> GetPersonsAsync()
        {
            var persons = _personRepository.GetPersons();

            if (!ModelState.IsValid)
            {
                await LogToDataBaseAsync("GetPersonsAsync", null, "Invalid ModelState");
                
                return BadRequest(ModelState);
            }

            return Ok(persons);
        }

        [HttpGet("GetPersonCount")]
        [ProducesResponseType(200, Type = typeof(PersonCount))]
        public async Task<IActionResult> GetPersonCountAsync()
        {
            var personCount = _personRepository.GetPersonCount();

            if (!ModelState.IsValid)
            {
                await LogToDataBaseAsync("GetPersonsAsync", null, "Invalid ModelState");

                return BadRequest(ModelState);
            }

            return Ok(personCount);
        }

        [HttpGet("GetPersonsPaged/{pageNum}")]
        [ProducesResponseType(200, Type = typeof(ICollection<PersonDto>))]
        public async Task<IActionResult> GetPersonsPagedAsync(int pageNum)
        {
            IActionResult ret;
            try
            {
                var personPage = _personRepository.GetPersonsPaged(pageNum);
                ret = Ok(personPage);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("GetPersonsPagedAsync", $"pageNum: {pageNum}", ex.Message);

                ret = BadRequest(ex.Message);
            }

            return ret;
        }

        [HttpGet("GetPersons/Like")]
        [ProducesResponseType(200, Type = typeof(ICollection<PersonDto>))]
        public async Task<IActionResult> GetPersonsLikeAsync([FromQuery] PersonSearchLike person)
        {
            IActionResult ret;

            try
            {
               var result = _personRepository.GetPersonsLike(person);
               ret = Ok(result);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("GetPersonsLikeAsync", null, ex.Message);

                ret = BadRequest(ex.Message);
            }

            return ret;
        }

        [HttpGet("GetPersons/Detailed")]
        [ProducesResponseType(200, Type = typeof(ICollection<PersonDto>))]
        public async Task<IActionResult> GetPersonsDetailedAsync([FromQuery] PersonSearchDetailed person)
        {
            IActionResult ret;

            try
            {
                var result = _personRepository.GetPersonsDetailed(person);
                ret = Ok(result);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("GetPersonsDetailedAsync", null, ex.Message);

                ret = BadRequest(ex.Message);
            }

            return ret;
        }

        [HttpGet("GetPersonById/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<IActionResult> GetPersonByIdAsync(int id)
        {
            PersonDto person;

            try
            {
                person = _personRepository.GetPersonById(id);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("GetPersonByIdAsync", $"id: {id}", ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok(person);
        }

        [HttpPost("CreatePerson")]
        [ProducesResponseType(201, Type = typeof(PersonDto))]
        public async Task<IActionResult> CreatePersonAsync(CreatePersonDto person)
        {
            PersonDto? result;
            try
            {
                result = _personRepository.CreatePerson(person);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("CreatePersonAsync", null, ex.Message);

                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetPersonByIdAsync), new { id = result?.Id }, result);
        }

        [HttpPut("UpdatePerson/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<IActionResult> UpdatePersonAsync(int id, [FromBody] UpdatePersonDto person)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.UpdatePerson(id, person);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("UpdatePersonAsync", $"id: {id}", ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("AddPhones/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<IActionResult> AddPhonesAsync(int id, UpdatePhoneDto phones)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.AddPhones(id, phones);
            }
            catch(Exception ex)
            {
                await LogToDataBaseAsync("AddPhonesAsync", $"id: {id}",ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete("RemovePhones/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<IActionResult> RemovePhonesAsync(int id, UpdatePhoneDto phones)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.RemovePhones(id, phones);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("RemovePhonesAsync", $"id: {id}", ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("AddRelations/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<IActionResult> AddRelationsAsync(int id, UpdateRelationsDto relations)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.AddRelations(id, relations);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("AddRelationsAsync", $"id: {id}", ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete("RemoveRelations/{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<IActionResult> RemoveRelationsAsync(int id, UpdateRelationsDto relations)
        {
            PersonDto? result;

            try
            {
                result = _personRepository.RemoveRelations(id, relations);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("RemoveRelationsAsync", $"id: {id}", ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete("DeletePerson/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeletePersonAsync(int id)
        {
            try
            {
                _personRepository.DeletePerson(id);
            }
            catch (Exception ex)
            {
                await LogToDataBaseAsync("DeletePersonAsync", $"id: {id}", ex.Message);

                return BadRequest(ex.Message);
            }

            return Ok($"Person with id {id} deleted.");
        }

        private async Task LogToDataBaseAsync(string method, string? arguments, string message)
        {
            HttpContext.Request.EnableBuffering();

            var requestBody = HttpContext.Items["RequestBody"]?.ToString();

            var log = new ErrorLog
            {
                RequestTime = DateTime.Now,
                RequestBody = requestBody,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Method = method,
                Arguments = arguments,
                ErrorMessage = message,
            };

            await _logRepository.SaveErrorLogAsync(log);
        }

    }
}
