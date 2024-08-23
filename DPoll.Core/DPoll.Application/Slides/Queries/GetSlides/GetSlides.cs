using DPoll.Domain.Entities;
using MediatR;

namespace DPoll.Application.Slides.Queries.GetSlides;
public class GetSlidesQuery : IRequest<List<Slide>>
{
    public Guid PresentationId { get; init; }
}


