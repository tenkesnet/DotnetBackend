using System.ComponentModel;
using Tanulok.Entity;

namespace Tanulok.Helper
{
    public class TanarHelper
    {
        public static ValidationResult validateTanar(Tanar tanar)
        {
            ValidationResult result = new ValidationResult { Errors = new List<string>(), isValid = true };
            if (tanar.lakcim == null)
            {
                result.isValid = false;
                result.Errors.Add ("A lakcímet kötelező megadni!");
            }
            return result;
        }
    }
}
