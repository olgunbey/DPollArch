using DPoll.Application.Slides.Queries.GetSlideByIndex;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Slides.Queries.GetSlideByIndex;
internal sealed class GetSlideByIndexHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetSlideByIndexQuery, Slide>
{
    public async Task<Slide> Handle(GetSlideByIndexQuery request, CancellationToken cancellationToken)
    {
        var slide = await applicationDbContext.Slide
             .FirstOrDefaultAsync(y => y.PresentationId == request.PresentationId && y.Index == request.Index, cancellationToken);

        ArgumentNullException.ThrowIfNull(slide);
        return slide;
    }
}
