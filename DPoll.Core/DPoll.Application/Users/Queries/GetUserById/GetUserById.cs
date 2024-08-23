using DPoll.Domain.Entities;
using MediatR;

namespace DPoll.Application.Users.Queries.GetUserById;
public class GetUserByIdQuery : IRequest<User>
{
    public Guid Id { get; init; }
}

