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
        [CustomValidation(typeof(Person), nameof(ValidateName))]
        public string FirstName { get; set; }

        [Required]
        [CustomValidation(typeof(Person), nameof(ValidateName))]
        public string LastName { get; set; }

        [Required]
        [CustomValidation(typeof(Person), nameof(ValidateGender))]
        public Gender Gender { get; set; }

        [Required]
        [CustomValidation(typeof(Person), nameof(ValidateIdNum))]
        public string IdNum { get; set; }

        [Required]
        [CustomValidation(typeof(Person), nameof(ValidateAge))]
        public DateOnly DateOfBirth { get; set; }

        public ICollection<Phone> Phones { get; set; }

        public ICollection<PersonalRelation> PersonalRelations { get; set; }


        public static ValidationResult ValidateName(string name, ValidationContext context)
        {
            ValidationResult ret;


            if (string.IsNullOrEmpty(name) || (name.Length < 2 || name.Length > 50))
            {
                ret = new ValidationResult("First name and Last name must be between 2 and 50 characters.");
            }
            else
            {
                // ლათინური ასოების რეინჯი
                var latPattern = new Regex(@"^[A-Za-z]+$");

                // Unicode-ში ქართული ასოების რეინჯი
                var geoPattern = new Regex(@"^[\u10A0-\u10FF]+$");

                if (latPattern.IsMatch(name))
                {
                    ret = ValidationResult.Success;
                }
                else if (geoPattern.IsMatch(name))
                {
                    ret = ValidationResult.Success;
                }
                else
                {
                    ret = new ValidationResult("First name and last name must contain either only Latin or only Georgian letters.");
                }
            }

            return ret;
        }

        public static ValidationResult ValidateGender(Gender gender, ValidationContext context)
        {
            ValidationResult ret;

            switch (gender)
            {
                case Gender.Male:
                case Gender.Female:
                    ret = ValidationResult.Success;
                    break;

                default:
                    ret = new ValidationResult("Invalid gender");
                    break;
            }

            return ret;
        }

        public static ValidationResult ValidateAge(DateOnly dateOfBirth, ValidationContext context)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now.AddYears(-age))) age--;

            return age >= 18 ? ValidationResult.Success : new ValidationResult("Person must be at least 18 years old.");
        }

        public static ValidationResult ValidateIdNum(string idNum, ValidationContext context)
        {
            ValidationResult ret;


            if (string.IsNullOrEmpty(idNum) || idNum.Length != 11)
            {
                ret = new ValidationResult("Identification Number must be 11 digits.");
            }
            else
            {
                // ციფრების რეინჯი
                var digitPattern = new Regex(@"^\d+$");

                ret = digitPattern.IsMatch(idNum) ? ValidationResult.Success : new ValidationResult("First name and last name must contain either only Latin or only Georgian letters.");
            }

            return ret;
        }
    }
}
