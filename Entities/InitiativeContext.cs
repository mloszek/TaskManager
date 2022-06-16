using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities
{
    public class InitiativeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Initiative> Initiatives { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Task> Tasks { get; set; }

        private const string _connection = "Server=tcp:taskmanagerdbserver123.database.windows.net,1433;Initial Catalog=TaskManager_db;Persist Security Info=False;User ID=mloszek;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Initiative>()
                .HasOne(c => c.CreatedBy);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role);

            modelBuilder.Entity<Initiative>()
                .HasMany(p => p.Epics)
                .WithOne(e => e.Initiative);

            modelBuilder.Entity<Epic>()
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Epic);

            modelBuilder.Entity<Task>()
                .HasOne(e => e.Status);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }
    }
}
