using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity2026.Web.Validations
{
    public class CheckStreetNr : ValidationAttribute, IClientModelValidator
    {
        private readonly int _max;

        public CheckStreetNr(int max)
        {
            _max = max; 
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-streetnr", "Not a valid street nr");
            context.Attributes.Add("data-val-streetnr-max", $"{_max}"); 
        }

        public override bool IsValid(object? value)
        {
            if(value is string input)
            {
                var num = input.Trim().Split().Last();
                return int.TryParse(num, out int res) & res <= _max; 
            }
            return false; 
        }
    }
}
