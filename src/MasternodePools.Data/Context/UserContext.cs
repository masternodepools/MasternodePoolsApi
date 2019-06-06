using MasternodePools.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasternodePools.Data.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
    }
}
