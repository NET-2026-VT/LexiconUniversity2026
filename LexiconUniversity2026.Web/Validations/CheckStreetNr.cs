using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity2026.Web.Validations
{
    public class CheckStreetNr : ValidationAttribute
    {
        private readonly int _max;

        public CheckStreetNr(int max)
        {
            _max = max; 
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
