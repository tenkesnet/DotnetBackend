using Tanulok.Entity;

namespace Tanulok.Repository
{
    public interface ITanuloRepository
    {
        public Task<IEnumerable<Tanulo>> GetTanulok();
        public Task<int> setTanulo(Tanulo tanulo);
        public Tanulo getTanuloByObject(Tanulo tanulo);
    }
}
