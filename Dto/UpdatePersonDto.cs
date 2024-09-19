namespace SkepERP.Dto
{
    public class UpdatePersonDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? Gender { get; set; }

        public string? IdNum { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public ICollection<CreatePhoneDto>? Phones { get; set; }
    }
}
