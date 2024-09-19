namespace SkepERP.Dto
{
    public class CreatePersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }

        public string IdNum { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public ICollection<CreatePhoneDto>? Phones { get; set; }

        public override string ToString()
        {
            return $"CreatePersonDto: [ FirstName: {FirstName}, LastName: {LastName}, Gender: {Gender}, IdNum: {IdNum}, DateOfBirth: {DateOfBirth}, Phones: {Phones?.ToString()} ]";
        }
    }
}
