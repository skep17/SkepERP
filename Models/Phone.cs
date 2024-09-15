using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int Id { get; set; }

        [Required]
        [CustomValidation(typeof(Phone), nameof(ValidateType))]
        public PhoneType Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Phone number must be between 4 and 50 characters.")]
        public string Number { get; set; }

        [Required]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

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
