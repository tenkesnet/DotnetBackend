using Dapper;
using Tanulok.Entity;
using Tanulok.Models;

namespace Tanulok.Repository
{
    public class TanuloRepository : ITanuloRepository
    {
        private readonly DapperContext _context;
        public TanuloRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Tanulo>> GetTanulok()

        {
            var query = "select l.*,t.* from tanulok t JOIN lakcim l on t.lakcim_id=l.id";
            using (var connection = _context.CreateConnection())
            {
                var tanulok = await connection.QueryAsync<Lakcim, Tanulo, Tanulo>(query, (l, t) =>
                {
                    t.lakcim = l;
                    return t;
                });
                return tanulok;
            }
            return null;

        }
        public async Task<int> setTanulo(Tanulo tanulo)
        {
            int id = _context.connection.QuerySingle<int>("Insert into tanulok (name, szuldatum, nem, tanatlag, lakcim_id) " +
                                "values (@name, @szuldatum, @nem, @tanatlag, @lakcim_id);" +
                                "select seq from sqlite_sequence WHERE name='tanulok';", new
                                {
                                    name = tanulo.name,
                                    szuldatum = tanulo.szulDatum,
                                    nem = tanulo.nem,
                                    tanatlag = tanulo.tanAtlag,
                                    lakcim_id = tanulo.lakcim.id
                                });
            return id;
        }
        public Tanulo getTanuloByObject(Tanulo tanulo)
        {
            Tanulo result = _context.connection.QueryFirstOrDefault<Tanulo>("select * from tanulok where name=@name " +
                "and szuldatum=@szuldatum and nem=@nem and tanatlag=@tanatlag;",
                new
                {
                    name = tanulo.name,
                    szuldatum = tanulo.szulDatum,
                    nem = tanulo.nem,
                    tanatlag = tanulo.tanAtlag,
                });
            return result;
         }
    }
}
