using Dapper;
using Tanulok.Entity;

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
        public async Task<int> setTanulo(Tanulo tanulo)
        {
            int id = _context.connection.QuerySingle<int>("Insert into tanulok (name, szuldatum, nem, tanatlag) " +
                                "values (@name, @szuldatum, @nem, @tanatlag);" +
                                "select seq from sqlite_sequence WHERE name='tanulok';", new
                                {
                                    name = tanulo.tanAtlag,
                                    szuldatum = tanulo.tanAtlag,
                                    nem = tanulo.nem,
                                    fotantargy = tanulo.tanAtlag
                                });
            return id;
        }
    }
}
