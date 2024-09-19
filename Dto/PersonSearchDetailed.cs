namespace SkepERP.Dto
{
    public class PersonSearchDetailed
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }

        public string IdNum { get; set; }

        public DateOnly DateOfBirth { get; set; }
    }
}
