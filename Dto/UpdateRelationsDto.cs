using SkepERP.Models;

namespace SkepERP.Dto
{
    public class UpdateRelationsDto
    {
        public ICollection<CreateRelationsDto>? Relations { get; set; }

        public override string ToString()
        {
            return $"UpdateRelationDto: {Relations?.ToString()}";
        }
    }
}
