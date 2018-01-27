using Microsoft.EntityFrameworkCore;
using Augmentr.Dal.Models;

namespace Augmentr.Dal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
