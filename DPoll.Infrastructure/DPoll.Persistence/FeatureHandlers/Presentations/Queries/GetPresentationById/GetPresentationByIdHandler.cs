using DPoll.Application.Presentations.Queries.GetPresentationById;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Presentations.Queries.GetPresentationById;
internal sealed class GetPresentationByIdHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetPresentationByIdQuery, Presentation>
{
    public async Task<Presentation> Handle(GetPresentationByIdQuery command, CancellationToken cancellationToken)
    {

        var result = await applicationDbContext.Presentation
            .FirstOrDefaultAsync(y => y.Id == command.Id, cancellationToken);


        ArgumentNullException.ThrowIfNull(result);

        return result;
    }
}