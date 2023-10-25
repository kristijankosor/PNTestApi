using PNTestApi.DTOs;
using PNTestApi.Models;

namespace PNTestApi.Interfaces
{
    public interface IQueryRepository
    {
        ICollection<Query> GetQueries();
        Task AddQueryAsync(Query query);
    }
}