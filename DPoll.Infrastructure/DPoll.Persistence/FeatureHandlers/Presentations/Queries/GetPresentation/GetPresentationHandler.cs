using DPoll.Application.Presentations.Queries.GetPresentation;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace DPoll.Persistence.FeatureHandlers.Presentations.Queries.GetPresentation;
internal sealed class GetPresentationsHandler(IApplicationDbContext appplicationDbContext) : IRequestHandler<GetPresentationsQuery, List<Presentation>>
{
    public async Task<List<Presentation>> Handle(GetPresentationsQuery request, CancellationToken cancellationToken)
    {
        return await appplicationDbContext.Presentation
              .ToListAsync(cancellationToken);
    }
}