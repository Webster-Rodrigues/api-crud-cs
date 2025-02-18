using System.ComponentModel.DataAnnotations;

namespace APICatalago.Validations;

public class FirstUpperLetter: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }
        
        var firstLetter = value.ToString()[0].ToString();
        if (firstLetter != firstLetter.ToUpper())
        {
            return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula");
        }
        
        return ValidationResult.Success;
    }
    
}