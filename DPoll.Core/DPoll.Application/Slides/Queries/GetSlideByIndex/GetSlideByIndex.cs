using DPoll.Domain.Entities;
using MediatR;

namespace DPoll.Application.Slides.Queries.GetSlideByIndex;
public class GetSlideByIndexQuery : IRequest<Slide>
{
    public Guid PresentationId { get; init; }
    public int Index { get; set; }
}
