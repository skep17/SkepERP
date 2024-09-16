using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkepERP.Models
{
    public enum RelationType
    {
        Other = 1,
        Colleague = 2,
        Acquaintance = 3,
        Relative = 4
    };

    public class PersonalRelation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [CustomValidation(typeof(PersonalRelation), nameof(ValidateType))]
        public RelationType Type { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int RelatedPersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("RelatedPersonId")]
        public virtual Person RelatedPerson { get; set; }

        public static ValidationResult ValidateType(RelationType type, ValidationContext context)
        {
            ValidationResult ret;

            switch (type)
            {
                case RelationType.Other:
                case RelationType.Colleague:
                case RelationType.Acquaintance:
                case RelationType.Relative:
                    ret = ValidationResult.Success;
                    break;

                default:
                    ret = new ValidationResult("Invalid relation type");
                    break;
            }

            return ret;
        }
    }
}
