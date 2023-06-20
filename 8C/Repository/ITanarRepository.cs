using Tanulok.Entity;

namespace Tanulok.Repository
{
    public interface ITanarRepository
    {
        public Task<IEnumerable<Tanar>> GetTanarok();
        public Task<int> setTanar(Tanar tanar);
    }
}
