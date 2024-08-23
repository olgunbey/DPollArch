using DPoll.Application.Users.Queries.GetUsers;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace DPoll.Persistence.FeatureHandlers.Users.Queries.GetUsers;
internal sealed class GetUsersHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetUsersQuery, List<User>>
{
    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await applicationDbContext.User
              .ToListAsync(cancellationToken);
    }
}

