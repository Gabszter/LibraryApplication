using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarianApplication.Blazor.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReaderNumber { get; set; }
        [Required]
        [NameValidation]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }

    public class NameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string name = (string)value;

            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult("Name can t be empty");
            }

            // Különleges karakterek ellenőrzése
            if (name.IndexOfAny("^[^!?_-:;#]+$".ToCharArray()) != -1)
            {
                return new ValidationResult("Invalid character in the name.");
            }

            return ValidationResult.Success;
        }
    }
}
