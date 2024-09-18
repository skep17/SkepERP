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
        public PhoneType Type { get; set; }

        [Required]
        public string Number { get; set; }

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public static string ValidateType(PhoneType type)
        {
            string ret;

            switch (type)
            {
                case PhoneType.Mobile:
                case PhoneType.Office:
                case PhoneType.Home:
                    ret = string.Empty;
                    break;

                default:
                    ret = "Invalid phone type";
                    break;
            }

            return ret;
        }

        public static string ValidateNumber(string number)
        {
            return number.Length >= 4 && number.Length <= 50 ? string.Empty : "Phone number must be between 4 and 50 characters.";
        }

        public string Validate()
        {
            string ret = Phone.ValidateType(Type);

            if (!string.IsNullOrEmpty(ret)) return ret;

            return ValidateNumber(Number);
        }
    }
}
