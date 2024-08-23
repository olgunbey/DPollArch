using DPoll.Domain.Entities;
using MediatR;

namespace DPoll.Application.Users.Queries.GetUsers;
public class GetUsersQuery : IRequest<List<User>> { }
