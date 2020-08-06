using Microsoft.EntityFrameworkCore;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    }   
}