using Tanulok.Models;

namespace Tanulok.Repository
{
    public interface IAlkalmazottRepository
    {
        public Task<IEnumerable<Alkalmazott>> GetAlkalmazottbyAuto();
        public Task<IEnumerable<Alkalmazott>> GetAlkalmazottbyReszleg();
        /*public Task<int> setTanar(Tanar tanar);

        public Tanar getTanarByObject(Tanar tanar);*/
    }
}
