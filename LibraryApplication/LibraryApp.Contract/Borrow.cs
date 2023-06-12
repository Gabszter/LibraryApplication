// <copyright file="Borrow.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Contract
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a borrow of a book by a reader.
    /// </summary>
    public class Borrow
    {
        /// <summary>
        /// Gets or sets the ID of the borrow.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reader number.
        /// </summary>
        [Required]
        public int ReaderNumber { get; set; }

        /// <summary>
        /// Gets or sets the inventory number of the borrowed book.
        /// </summary>
        [Required]
        public int InventoryNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the book was borrowed.
        /// </summary>
        [Required]
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Gets or sets the return date for the borrowed book.
        /// </summary>
        [Required]
        [LaterThan(nameof(BorrowDate))]
        public DateTime ReturnDate { get; set; }
    }
}

/// <summary>
/// Represents a custom validation attribute that ensures a DateTime property is later than another DateTime property.
/// </summary>
public class LaterThanAttribute : ValidationAttribute
{
    private readonly string _otherProperty;

    /// <summary>
    /// Initializes a new instance of the <see cref="LaterThanAttribute"/> class.
    /// </summary>
    /// <param name="otherProperty">The name of the other DateTime property.</param>
    public LaterThanAttribute(string otherProperty)
    {
        _otherProperty = otherProperty;
    }

    /// <summary>
    /// Determines whether the specified value of the object is valid.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var earlierDate = validationContext.ObjectType.GetProperty(_otherProperty).GetValue(validationContext.ObjectInstance, null);

        if (!(earlierDate is DateTime) || !(value is DateTime))
        {
            return new ValidationResult("The properties must be of type DateTime.");
        }

        if ((DateTime)value <= (DateTime)earlierDate)
        {
            return new ValidationResult("The return date must be later than the borrow date.");
        }

        return ValidationResult.Success;
    }
}
