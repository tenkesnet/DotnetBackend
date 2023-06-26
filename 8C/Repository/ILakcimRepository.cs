using Tanulok.Entity;

namespace Tanulok.Repository
{
    public interface ILakcimRepository
    {
        public Task<IEnumerable<Lakcim>> GetLakcim();
        public Task<long> setLakcim(Lakcim lakcim);
        public Lakcim getLakcimByObject(Lakcim lakcim);
    }
}
