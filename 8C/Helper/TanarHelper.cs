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
            ValidationResult<Tanar> validationResultSzemely = SzemelyHelper.validateSzemely(tanar);
            ValidationResult<Tanar> validationResultLakcim = LakcimHelper.validateLakcim(tanar.lakcim);
            ValidationResult<Tanar> result = new ValidationResult<Tanar> { Errors = new List<string>(), isValid = true };
            result.Errors.AddRange(validationResultSzemely.Errors);
            if (tanar.foTantargy == null || tanar.foTantargy == "")
            {
                result.isValid = false;
                result.Errors.Add("A főtantárgyat kötelező megadni!");
            }
            result.Errors.AddRange(validationResultLakcim.Errors);
            if (result.isValid)
            {
                Lakcim lakcim = lakcimRepository.getLakcimByObject(tanar.lakcim);
                if (lakcim == null)
                {
                    long lakcimId = await lakcimRepository.setLakcim(tanar.lakcim);
                    long tanarId = await tanarRepository.setTanar(tanar);
                    tanar.id = tanarId;
                    tanar.lakcim.id = lakcimId;
                    result.result = tanar;
                }
                else
                {
                    long tanarId = await tanarRepository.setTanar(tanar);
                    tanar.id = tanarId;
                    tanar.lakcim = lakcim;
                    result.result = tanar;
                }
            }
            return result;
        }
    }
} 
