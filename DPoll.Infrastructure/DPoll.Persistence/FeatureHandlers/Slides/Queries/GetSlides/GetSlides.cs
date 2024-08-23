using DPoll.Application.Slides.Queries.GetSlides;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Slides.Queries.GetSlides;
internal sealed class GetSlidesHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetSlidesQuery, List<Slide>>
{
    public async Task<List<Slide>> Handle(GetSlidesQuery request, CancellationToken cancellationToken)
    {
        var slides = await applicationDbContext.Slide
            .Where(y => y.PresentationId == request.PresentationId)
            .ToListAsync();
        return slides;
    }
}