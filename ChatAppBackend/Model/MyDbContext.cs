using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml;

namespace ChatAppBackend.Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Messages>()
                .HasKey(e => e.Id);

        }

        //public class MyEntity
        //{
        //    public int Id { get; set; }
        //    public string UserName { get; set; }
        //    public string Password { get; set; }
        //    public string Email { get; set; }

        //}
        public DbSet<User> User { get; set; }

        public DbSet<Messages> Messages { get; set; }
    }
}
