using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AppCore.Business.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class StringToDecimalAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        bool validationResult;
        if (value == null)
        {
            validationResult = true;
        }
        else
        {
            string valueText = value.ToString().Trim().Replace(",", ".");
            validationResult = decimal.TryParse(valueText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);
        }

        return validationResult;
    }
}