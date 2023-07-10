using Tanulok.Entity;
using Tanulok.Repository;

namespace Tanulok.Helper
{
    public class SzemelyHelper
    {
        public static ValidationResult<Szemely> validateSzemely(Szemely szemely)
        {
            ValidationResult<Szemely> result = new ValidationResult<Szemely> { Errors = new List<string>(), isValid = true };
            if (szemely.name == "" || szemely.name == null)
            {
                result.isValid = false;
                result.Errors.Add("A nevet kötelező megadni!");
            }
            if (szemely.szulDatum == null)
            {
                result.isValid = false;
                result.Errors.Add("A születési dátum kötelező");
            }
            if (szemely.nem == "" || szemely.nem == null)
            {
                result.isValid = false;
                result.Errors.Add("A nemet kötelező megadni!");
            }
            return result;
        }
    }

}

