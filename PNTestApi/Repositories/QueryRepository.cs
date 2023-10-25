using PNTestApi.Data;
using PNTestApi.Models;
using PNTestApi.Interfaces;

namespace PNTestApi.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly DataContext _context;

        public QueryRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Query> GetQueries()
        {
            return _context.Queries.OrderBy(o => o.Id).ToList();
        }

        public async Task AddQueryAsync(Query query)
        {
            _context.Queries.Add(query);
            await _context.SaveChangesAsync();
        }
    }
}
