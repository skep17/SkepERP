using Microsoft.AspNetCore.Mvc;
using SkepERP.Models;

namespace SkepERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataInfoController : ControllerBase
    {
        [HttpGet("GetGenderTypes")]
        public IActionResult GetGenderTypes()
        {
            Dictionary<int,string> enumDict = new Dictionary<int,string>();
            var enumValues = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            foreach (var enumValue in enumValues)
            {
                enumDict.Add((int)enumValue, enumValue.ToString());
            }
            return Ok(enumDict);
        }

        [HttpGet("GetRelationTypes")]
        public IActionResult GetRelationTypes()
        {
            Dictionary<int, string> enumDict = new Dictionary<int, string>();
            var enumValues = Enum.GetValues(typeof(RelationType)).Cast<RelationType>();
            foreach (var enumValue in enumValues)
            {
                enumDict.Add((int)enumValue, enumValue.ToString());
            }
            return Ok(enumDict);
        }

        [HttpGet("GetPhoneTypes")]
        public IActionResult GetPhoneTypes()
        {
            Dictionary<int, string> enumDict = new Dictionary<int, string>();
            var enumValues = Enum.GetValues(typeof(PhoneType)).Cast<PhoneType>();
            foreach (var enumValue in enumValues)
            {
                enumDict.Add((int)enumValue, enumValue.ToString());
            }
            return Ok(enumDict);
        }
    }
}
