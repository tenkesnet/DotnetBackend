using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using Tanulok.Entity;
using Tanulok.Models;

namespace Tanulok.Repository
{
    public class AlkalmazottEFRepository : IAlkalmazottRepository
    {
        private readonly IskolaContext _context;
        public AlkalmazottEFRepository(IskolaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Alkalmazott>> GetAlkalmazott()
        {
            return _context.Alkalmazotts.ToList();

        }

    }
}
