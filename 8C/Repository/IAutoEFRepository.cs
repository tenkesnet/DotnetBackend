using Tanulok.Models;

namespace Tanulok.Repository
{
    public interface IAutoEFRepository
    {
        public Task<IEnumerable<Autok>> GetAutoByRendeles();
        public Task<List<Autok>> GetAutoByAlkalmazott();
        public Task<IEnumerable<Autok>> GetAutoByTipusok();
    }
}
