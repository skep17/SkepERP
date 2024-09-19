using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public RelationType Type { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int RelatedPersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("RelatedPersonId")]
        public virtual Person RelatedPerson { get; set; }

        public static string ValidateType(RelationType type)
        {
            string ret;

            switch (type)
            {
                case RelationType.Other:
                case RelationType.Colleague:
                case RelationType.Acquaintance:
                case RelationType.Relative:
                    ret = string.Empty;
                    break;

                default:
                    ret = "Invalid relation type";
                    break;
            }

            return ret;
        }

        public string Validate()
        {
            string ret = PersonalRelation.ValidateType(Type);

            return ret;
        }
    }
}
