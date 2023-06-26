using Tanulok.Entity;

namespace Tanulok.Helper
{
    public class TanuloHelper
    {
        /*public static ValidationResult validateTanulo(Tanulo tanulo)
        {
            ValidationResult validationResultSzemely = SzemelyHelper.validateSzemely(tanulo);
            ValidationResult validationResultLakcim = LakcimHelper.validateLakcim(tanulo.lakcim);
            ValidationResult result = new ValidationResult { Errors = new List<string>(), isValid = true };
            result.Errors.AddRange(validationResultSzemely.Errors);
            if (tanulo.tanAtlag == null || tanulo.tanAtlag ==0)
            {
                result.isValid = false;
                result.Errors.Add("A tanuló átlagát kötelező megadni!");
            }
            result.Errors.AddRange(validationResultLakcim.Errors);
            return result;
        }*/
    }
}
