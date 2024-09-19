using SkepERP.Models;
using System.Diagnostics.Contracts;

namespace SkepERP.Dto
{
    public class UpdatePhoneDto
    {
        public ICollection<CreatePhoneDto>? Phones { get; set; }

        public override string ToString()
        {
            return $"UpdatePhoneDto: {Phones?.ToString()}";
        }
    }
}
