using DPoll.Application.Users.Queries.GetUserById;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Users.Queries.GetUserById;
internal sealed class GetUserByIdHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var User = await applicationDbContext.User
             .SingleAsync(y => y.Id == request.Id, cancellationToken);
            return User;
        }
        catch (Exception)
        {
            throw;
        }

    }
}
