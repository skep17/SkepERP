using System.ComponentModel.DataAnnotations;

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
        public int Id { get; set; }

        public RelationType RelationType { get; set; }

        public int PersonId { get; set; }

        public int PersonOtherId { get; set; }

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
