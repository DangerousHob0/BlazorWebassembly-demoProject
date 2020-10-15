using Microsoft.EntityFrameworkCore;
using DataModel.Models;

namespace WebApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
        : base(opts) { }
        public DbSet<TodoItem> Tasks { get; set; }
    }
}