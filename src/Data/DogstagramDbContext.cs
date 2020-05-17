namespace Dogstagram.Data
{
    using Dogstagram.Data.Model;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DogstagramDbContext : IdentityDbContext<User>
    {
        public DogstagramDbContext(DbContextOptions<DogstagramDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
          builder
            .Entity<Dog>()
            .HasOne(c => c.User)
            .WithMany(u => u.Dogs)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

          base.OnModelCreating(builder);
        }
    }
}
