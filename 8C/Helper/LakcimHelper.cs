using System.Reflection.Metadata.Ecma335;
using Tanulok.Entity;
using Tanulok.Repository;
using Tanulok.Service;

namespace Tanulok.Helper
{
    

    public class LakcimHelper
    {    
        public static ValidationResult<Tanar> validateLakcim(Lakcim lakcim)

        {
            ValidationResult<Tanar> resultLakcim = new ValidationResult<Tanar> { Errors = new List<string>(), isValid = true };
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
            return resultLakcim;
        }
        public static int idLakcim(Lakcim lakcim)
        {
            int id = 0;
            
            return id;
        }
    }
}
