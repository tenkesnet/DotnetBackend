using Dapper;
using Tanulok.Entity;

namespace Tanulok.Repository
{
    public class SzemelyRepository
    {
        private readonly DapperContext _context;
        public SzemelyRepository(DapperContext context)
        {
            _context = context;
        }
        public SzemelyRepository()
        {
            
        }
 
    }
}
