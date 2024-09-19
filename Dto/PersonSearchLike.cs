using SkepERP.Models;

namespace SkepERP.Dto
{
    public class PersonSearchLike
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdNum { get; set; }

        public override string ToString()
        {
            return $"PersonSearchLike: [ FirstName: {FirstName}, LastName: {LastName}, IdNum: {IdNum} ]";
        }
    }
}
