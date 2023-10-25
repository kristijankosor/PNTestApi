using Microsoft.EntityFrameworkCore;
using PNTestApi.Models;

namespace PNTestApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }
        
        public DbSet<Query> Queries { get; set; }
    }
}
