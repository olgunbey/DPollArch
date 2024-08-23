using DPoll.Domain.Entities;
using MediatR;


namespace DPoll.Application.Presentations.Queries.GetPresentationById;
public class GetPresentationByIdQuery : IRequest<Presentation>
{
    public Guid Id { get; init; }
}
