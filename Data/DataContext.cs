using Microsoft.EntityFrameworkCore;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Data {
    public class DataContext : DbContext {
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterSkill> CharacterSkills { get; set; }
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<CharacterSkill> ()
                .HasKey (cs => new { cs.CharacterId, cs.SkillId });
        }
    }
}