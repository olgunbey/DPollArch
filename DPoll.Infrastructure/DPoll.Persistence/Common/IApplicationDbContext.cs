using DPoll.Domain.Common;
using DPoll.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DPoll.Persistence.Common;
public interface IApplicationDbContext
{
    public DbSet<Slide> Slide { get; }
    public DbSet<User> User { get; }
    public DbSet<Presentation> Presentation { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
