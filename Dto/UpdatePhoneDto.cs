using SkepERP.Models;

namespace SkepERP.Dto
{
    public class UpdatePhoneDto
    {
        public ICollection<CreatePhoneDto>? Phones { get; set; }
    }
}
