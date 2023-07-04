using SQLitePCL;
using System.Reflection.Metadata.Ecma335;
using Tanulok.Entity;
using Tanulok.Repository;
using Tanulok.Service;

namespace Tanulok.Helper
{
    public class LakcimHelper
    {
        public static ValidationResult<Lakcim> validateLakcim(Lakcim lakcim, ILakcimRepository lakcimRepository)

        {
            Lakcim lakcimVane = lakcimRepository.getLakcimByObject(lakcim);
            
            ValidationResult<Lakcim> resultLakcim = new ValidationResult<Lakcim> { Errors = new List<string>(), isValid = true };
            if (lakcimVane != null)
            {
                resultLakcim.Errors.Add("Ilyen lakcím már létezik");
                resultLakcim.result = lakcimVane;
                resultLakcim.isValid = true;
                return resultLakcim;
            }
            if (lakcim.telepules == "" || lakcim.telepules == null)
            {
                resultLakcim.isValid = false;
                resultLakcim.Errors.Add("A települést kötelező megadni!");
            }
            if (lakcim.irszam < 1000 || lakcim.irszam > 10000)
            {
                resultLakcim.isValid = false;
                resultLakcim.Errors.Add("Az irányítószám nem megfelelő!");
            }
            if (lakcim.utca == "" || lakcim.utca == null)
            {
                resultLakcim.isValid = false;
                resultLakcim.Errors.Add("Az utcát kötelező megadni!");
            }
            if (lakcim.hazszam == 0)
            {
                resultLakcim.isValid = false;
                resultLakcim.Errors.Add("A házszám nem lehet 0!");
            }
            resultLakcim.result = lakcim;
            return resultLakcim;
        }
    }
}
