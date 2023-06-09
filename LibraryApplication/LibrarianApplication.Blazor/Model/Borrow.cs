using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarianApplication.Blazor.Model
{
    public class Borrow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ReaderNumber { get; set; }
        [Required]
        public int InventoryNumber { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        [LaterThan(nameof(BorrowDate))]
        public DateTime ReturnDate { get; set; }
    }
}

public class LaterThanAttribute : ValidationAttribute
{
    private readonly string _otherProperty;

    public LaterThanAttribute(string otherProperty)
    {
        _otherProperty = otherProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var earlierDate = validationContext.ObjectType.GetProperty(_otherProperty).GetValue(validationContext.ObjectInstance, null);

        if (!(earlierDate is DateTime) || !(value is DateTime))
        {
            return new ValidationResult("The properties must be of type DateTime.");
        }

        if ((DateTime)value <= (DateTime)earlierDate)
        {
            return new ValidationResult("The return date variable must be greater than the borrow date.");
        }

        return ValidationResult.Success;
    }
}
