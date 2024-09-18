using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace SkepERP.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
    };

    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string IdNum { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public ICollection<Phone> Phones { get; set; }

        public ICollection<PersonalRelation> PersonalRelations { get; set; }


        public static string ValidateName(string name)
        {
            string ret;


            if (string.IsNullOrEmpty(name) || (name.Length < 2 || name.Length > 50))
            {
                ret = "First name and Last name must be between 2 and 50 characters.";
            }
            else
            {
                // ლათინური ასოების რეინჯი
                var latPattern = new Regex(@"^[A-Za-z]+$");

                // Unicode-ში ქართული ასოების რეინჯი
                var geoPattern = new Regex(@"^[\u10A0-\u10FF]+$");

                if (latPattern.IsMatch(name) || geoPattern.IsMatch(name))
                {
                    ret = string.Empty;
                }
                else
                {
                    ret = "First name and last name must contain either only Latin or only Georgian letters.";
                }
            }

            return ret;
        }

        public static string ValidateGender(Gender gender)
        {
            string ret;

            switch (gender)
            {
                case Gender.Male:
                case Gender.Female:
                    ret = string.Empty;
                    break;

                default:
                    ret = "Invalid gender";
                    break;
            }

            return ret;
        }

        public static string ValidateAge(DateOnly dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now.AddYears(-age))) age--;

            return age >= 18 ? string.Empty : "Person must be at least 18 years old.";
        }

        public static string ValidateIdNum(string idNum)
        {
            string ret;


            if (string.IsNullOrEmpty(idNum) || idNum.Length != 11)
            {
                ret = "Identification Number must be 11 digits.";
            }
            else
            {
                // ციფრების რეინჯი
                var digitPattern = new Regex(@"^\d+$");

                ret = digitPattern.IsMatch(idNum) ? string.Empty : "Identification Number must contain only digits.";
            }

            return ret;
        }

        public string Validate()
        {
            string ret = Person.ValidateName(FirstName);

            if (!string.IsNullOrEmpty(ret)) return ret;

            ret = Person.ValidateName(LastName);

            if (!string.IsNullOrEmpty(ret)) return ret;

            ret = Person.ValidateGender(Gender);

            if (!string.IsNullOrEmpty(ret)) return ret;

            ret = Person.ValidateAge(DateOfBirth);

            if (!string.IsNullOrEmpty(ret)) return ret;

            ret = Person.ValidateIdNum(IdNum);

            return ret;
        }
    }
}
