using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity2026.Web.Validations
{
    public class CheckLastName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            const string errorMessage = "First name and last name must be different!";

            //var logger = (ILogger<CheckLastName>?)validationContext.GetService(typeof(ILogger<CheckLastName>));
            //logger?.LogInformation("Validating last name");

            if(value is string input)
            {
                if(validationContext.ObjectInstance is StudentCreateViewModel model)
                {
                    return input != model.FirstName ?
                        ValidationResult.Success :
                        new ValidationResult(errorMessage);
                }
            }
            return new ValidationResult("Something went wrong!"); 
        }        
    }
}
