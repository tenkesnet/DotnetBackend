using Dapper;
using Tanulok.Model;

namespace Tanulok.Repository
{
    public class TanarRepository :ITanarRepository
    {
        private readonly DapperContext _context;
        public TanarRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tanar>> GetTanarok()
        {
                var query = "SELECT * FROM tanarok";
                using (var connection = _context.CreateConnection())
                {
                    var companies = await connection.QueryAsync<Tanar>(query);
                    return companies.ToList();
                }
            }
    }
}
