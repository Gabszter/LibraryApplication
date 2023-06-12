// <copyright file="Person.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApp.Contract
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the reader number of the person.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReaderNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        [Required]
        [NameValidation]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the person.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }
    }

    /// <summary>
    /// Represents a custom validation attribute for name validation.
    /// </summary>
    public class NameValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string name = (string)value;

            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult("Name cannot be empty.");
            }

            // Validate special characters
            if (name.IndexOfAny("^[^!?_-:;#]+$".ToCharArray()) != -1)
            {
                return new ValidationResult("Invalid character in the name.");
            }

            return ValidationResult.Success;
        }
    }
}
