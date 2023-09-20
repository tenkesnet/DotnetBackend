using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Tanulok.Entity;
using Tanulok.Models;

namespace Tanulok.Repository
{
    public class AutoEFRepository : IAutoEFRepository
    {
        private readonly IskolaContext _context;
        public AutoEFRepository(IskolaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Autok>> GetAutoByRendeles()
        {
            return _context.Autoks.Where(a=>a.Rendszam=="ABR-047").Include(a=>a.Rendeles).ToList();

        }

        public async Task<List<Autok>> GetAutoByAlkalmazott()
        {
            return _context.Autoks.Include(a => a.Alkalmazott).Include(a=>a.Tipusok).ToList();

        }

        public async Task<IEnumerable<Autok>> GetAutoByTipusok()
        {
            return _context.Autoks.Include(a => a.Tipusok).ToList();

        }

    }
}
