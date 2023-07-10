using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using Tanulok.Entity;

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
            var query = "select l.*,t.* from tanarok t JOIN lakcim l on t.lakcim_id=l.id";
            using (var connection = _context.CreateConnection())
            {
                var tanarok = await connection.QueryAsync<Lakcim, Tanar, Tanar>(query, (l, t) =>
                {
                    t.lakcim = l;
                    return t;
                });
                return tanarok;
            }
        }

        public async Task<int> setTanar(Tanar tanar)
        {
            int id = _context.connection.QuerySingle<int>("Insert into tanarok (name, szuldatum, nem, fotantargy, lakcim_id) " +
                                "values (@name, @szuldatum, @nem, @fotantargy, @lakcim_id);" +
                                "select seq from sqlite_sequence WHERE name='tanarok';", new
                                {
                                    name = tanar.name,
                                    szuldatum = tanar.szulDatum,
                                    nem = tanar.nem,
                                    fotantargy = tanar.foTantargy,
                                    lakcim_id = tanar.lakcim.id
                                });
            return id;
        }
        public Tanar getTanarByObject(Tanar tanar)
        {
            Tanar result = _context.connection.QueryFirstOrDefault<Tanar>("select * from tanarok where name=@name " +
                "and szuldatum=@szuldatum and nem=@nem and fotantargy=@fotantargy;",
                new
                {
                    name = tanar.name,
                    szuldatum = tanar.szulDatum,
                    nem = tanar.nem,
                    fotantargy = tanar.foTantargy,
                });
            return result;
        }
    }
}
