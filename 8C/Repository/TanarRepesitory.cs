using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
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

        public async Task<string> setTanar(Tanar tanar)
        {
                _context.connection.Execute("Insert into tanarok (name, szuldatum, nem, fotantargy) " +
                    "values (@name, @szuldatum, @nem, @fotantargy)", new
                    {
                        name = tanar.Name,
                        szuldatum = tanar.SzulDatum,
                        nem = tanar.Nem,
                        fotantargy = tanar.foTantargy
                    });
            return "Ok";
        }
    }
}
