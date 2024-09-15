using System.ComponentModel.DataAnnotations;

namespace SkepERP.Models
{
    public enum PhoneType
    {
        Mobile = 1,
        Office = 2,
        Home = 3,
    };

    public class Phone
    {
        public int Id { get; set; }

        public PhoneType Type { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Phone number must be between 4 and 50 characters.")]
        public string Number { get; set; }

        public int PersonId { get; set; }

        public static ValidationResult ValidateType(PhoneType type, ValidationContext context)
        {
            ValidationResult ret;

            switch (type)
            {
                case PhoneType.Mobile:
                case PhoneType.Office:
                case PhoneType.Home:
                    ret = ValidationResult.Success;
                    break;

                default:
                    ret = new ValidationResult("Invalid phone type");
                    break;
            }

            return ret;
        }
    }
}
