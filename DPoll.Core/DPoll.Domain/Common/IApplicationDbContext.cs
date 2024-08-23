using DPoll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace DPoll.Application.Common;

public interface IApplicationDbContext
{
    public DbSet<Slide> Slides{ get; }
    public DbSet<User> Users{ get; }
    public DbSet<Presentation> Presentations{ get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
