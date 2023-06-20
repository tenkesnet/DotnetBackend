using Tanulok.Entity;

namespace Tanulok.Repository
{
    public interface ITanuloRepository
    {
        public Task<IEnumerable<Tanulo>> GetTanulok();
    }
}
