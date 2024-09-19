namespace SkepERP.Dto
{
    public class RelationDto
    {
        public string RelationType { get; set; } = string.Empty;

        public int PersonId { get; set; }

        public override string ToString()
        {
            return $"RelationDto: [ RelationType: {RelationType}, PersonId: {PersonId} ]";
        }
    }
}
