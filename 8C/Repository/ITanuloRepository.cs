using Tanulok.Model;

namespace Tanulok.Repository
{
    public interface ITanuloRepository
    {
        public Task<IEnumerable<Tanulo>> GetTanulok();
    }
}
