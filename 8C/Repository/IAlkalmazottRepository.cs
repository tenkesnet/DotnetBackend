using Tanulok.Models;

namespace Tanulok.Repository
{
    public interface IAlkalmazottRepository
    {
        public Task<IEnumerable<Alkalmazott>> GetAlkalmazott();
        /*public Task<int> setTanar(Tanar tanar);

        public Tanar getTanarByObject(Tanar tanar);*/
    }
}
