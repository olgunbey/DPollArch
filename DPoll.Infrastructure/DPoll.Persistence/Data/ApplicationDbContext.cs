using DPoll.Domain.Common;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using DPoll.Persistence.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public DbSet<Slide> Slide => Set<Slide>();
        public DbSet<User> User => Set<User>();
        public DbSet<Presentation> Presentation => Set<Presentation>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PresentationConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
