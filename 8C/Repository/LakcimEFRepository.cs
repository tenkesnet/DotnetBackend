using Dapper;
using Tanulok.Entity;

namespace Tanulok.Repository
{
    public class LakcimEFRepository : ILakcimRepository
    {
        private readonly DapperContext _context;
        public LakcimEFRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Lakcim>> GetLakcim()
        {
            var query = "SELECT * FROM lakcim";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Lakcim>(query);
                return companies.ToList();
            }
        }

        public async Task<long> setLakcim(Lakcim lakcim)
        {
            long id = _context.connection.QuerySingle<long>("Insert into lakcim (telepules, irszam, utca, hazszam) " +
                                "values (@telepules, @irszam, @utca, @hazszam);" +
                                "select seq from sqlite_sequence WHERE name='lakcim';", new
                                {
                                    telepules = lakcim.telepules,
                                    irszam = lakcim.irszam,
                                    utca = lakcim.utca,
                                    hazszam = lakcim.hazszam,
                                });
            return id;
        }

        public Lakcim getLakcimByObject(Lakcim lakcim)
        {
            Lakcim result = _context.connection.QueryFirstOrDefault<Lakcim>("select * from lakcim where telepules=@telepules " +
                "and irszam=@irszam and utca=@utca and hazszam=@hazszam;",
                new
                {
                    telepules = lakcim.telepules,
                    irszam = lakcim.irszam,
                    utca = lakcim.utca,
                    hazszam = lakcim.hazszam,
                });

            return result;
        }
    }
}
