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
                var query = "SELECT * FROM tanarok";
                using (var connection = _context.CreateConnection())
                {
                    var companies = await connection.QueryAsync<Tanar>(query);
                    return companies.ToList();
                }
        }

        public async Task<int> setTanar(Tanar tanar)
        {
            int id = _context.connection.QuerySingle<int>("Insert into tanarok (name, szuldatum, nem, fotantargy) " +
                                "values (@name, @szuldatum, @nem, @fotantargy);" +
                                "select seq from sqlite_sequence WHERE name='tanarok';", new
                                {
                                    name = tanar.name,
                                    szuldatum = tanar.szulDatum,
                                    nem = tanar.nem,
                                    fotantargy = tanar.foTantargy
                                });
            return id;
        }
    }
}
