using System.ComponentModel;
using Tanulok.Entity;
using Tanulok.Repository;
using Tanulok.Service;

namespace Tanulok.Helper
{
    public class TanarHelper
    {
        public static async Task<ValidationResult<Tanar>> validateTanar(Tanar tanar, ILakcimRepository lakcimRepository,
            ITanarRepository tanarRepository)
        {
            Tanar tanarVane = tanarRepository.getTanarByObject(tanar);
            ValidationResult<Szemely> validationResultSzemely = SzemelyHelper.validateSzemely(tanar);
            ValidationResult<Lakcim> validationResultLakcim = LakcimHelper.validateLakcim(tanar.lakcim, lakcimRepository);
            ValidationResult<Tanar> result = new ValidationResult<Tanar> { Errors = new List<string>(), isValid = true };
            result.Errors.AddRange(validationResultSzemely.Errors);
            result.Errors.AddRange(validationResultLakcim.Errors);
            if (tanarVane != null)
            {
                result.Errors.Add("Ezzel a névvel és adatokkal már létezik tanár az adatbázisban!");
                result.result = tanarVane;
                result.isValid = false;
                return result;
            }
            if (validationResultSzemely.isValid == false || validationResultLakcim.isValid == false || result.isValid == false)
            {
                result.isValid = false;
            }
            if (tanar.foTantargy == null || tanar.foTantargy == "")
            {
                result.isValid = false;
                result.Errors.Add("A főtantárgyat kötelező megadni!");
                return result;
            }
            result.result = tanar;
            result.result.lakcim = validationResultLakcim.result == null ? tanar.lakcim : validationResultLakcim.result;
            return result;

        }
    }
}
