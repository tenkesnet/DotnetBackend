using Dapper;
using Tanulok.Model;

namespace Tanulok.Repository
{
    public class TanuloRepository :ITanuloRepository
    {
        private readonly DapperContext _context;
        public TanuloRepository(DapperContext context)
        {
            _context = context;
        }
    

        public async Task<IEnumerable<Tanulo>> GetTanulok()

        {
            var query = "SELECT nev as name FROM tanulok";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Tanulo>(query);
                return companies.ToList();
            }

        }
    }
}
