using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Alkalmazott>> GetAlkalmazottbyAuto()
        {
            return _context.Alkalmazotts.Include(a=>a.Autoks).ToList();

        }
        public async Task<IEnumerable<Alkalmazott>> GetAlkalmazottbyReszleg()
        {
            return _context.Alkalmazotts.Include(a=>a.Reszleg).ToList();

        }

    }
}
