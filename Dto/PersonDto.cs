using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkepERP.Dto
{

    public class PersonDto
    {
        public static readonly int PAGESIZE = 10;

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string IdNum { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public ICollection<PhoneDto>? Phones { get; set; }

        public ICollection<RelationDto>? PersonalRelations { get; set; }
    }
}
