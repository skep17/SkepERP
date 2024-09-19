namespace SkepERP.Dto
{
    public class CreateRelationsDto
    {
        public int RelationType { get; set; }
        public int PersonId { get; set; }

        public override string ToString()
        {
            return $"CreateRelationDto: [ RelationType: {RelationType}, PersonId: {PersonId} ]";
        }
    }
}
