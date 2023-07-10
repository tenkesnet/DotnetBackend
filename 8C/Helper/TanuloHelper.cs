using Tanulok.Entity;
using Tanulok.Repository;

namespace Tanulok.Helper
{
    public class TanuloHelper
    {
        public static async Task<ValidationResult<Tanulo>> validateTanulo(Tanulo tanulo, ILakcimRepository lakcimRepository, ITanuloRepository tanuloRepository)
        {
            Tanulo tanuloVane = tanuloRepository.getTanuloByObject(tanulo);
            ValidationResult<Szemely> validationResultSzemely = SzemelyHelper.validateSzemely(tanulo);
            ValidationResult<Lakcim> validationResultLakcim = LakcimHelper.validateLakcim(tanulo.lakcim, lakcimRepository);
            ValidationResult<Tanulo> result = new ValidationResult<Tanulo> { Errors = new List<string>(), isValid = true };
            result.Errors.AddRange(validationResultSzemely.Errors);
            result.Errors.AddRange(validationResultLakcim.Errors);
            if (tanuloVane != null)
            {
                result.Errors.Add("Ezzel a névvel és adatokkal már létezik tanulo az adatbázisban!");
                result.result = tanuloVane;
                result.isValid = false;
                return result;
            }
            if (validationResultSzemely.isValid == false || validationResultLakcim.isValid == false || result.isValid == false) 
            {
                result.isValid = false;            
            }
            if (tanulo.tanAtlag == null || tanulo.tanAtlag == 0)
            {
                result.isValid = false;
                result.Errors.Add("A tanuló átlagát kötelező megadni!");
                return result;
            }
            result.result = tanulo;
            result.result.lakcim = validationResultLakcim.result == null ? tanulo.lakcim : validationResultLakcim.result;
            return result;
        }   
    }
}
