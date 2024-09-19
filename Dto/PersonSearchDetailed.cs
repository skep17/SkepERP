using SkepERP.Models;

namespace SkepERP.Dto
{
    public class PersonSearchDetailed
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }

        public string IdNum { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"PersonSearchDetailed: [ FirstName: {FirstName}, LastName: {LastName}, Gender: {Gender}, IdNum: {IdNum}, DateOfBirth: {DateOfBirth} ]";
        }
    }
}
