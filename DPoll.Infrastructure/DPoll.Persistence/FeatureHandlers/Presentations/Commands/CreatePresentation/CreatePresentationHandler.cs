using DPoll.Application.Presentations.Commands.CreatePresentation;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Presentations.Commands.CreatePresentation;
internal sealed class CreatePresentationHandler(IApplicationDbContext applicationDbContext) : ICreatePresentationHandler, IRequestHandler<CreatePresentationCommand, Presentation> 
{
    public async Task<Presentation> Handle(CreatePresentationCommand command, CancellationToken cancellationToken)
    {

        var presentation = new Presentation(userId: command.UserId, title: command.Title);

        Guid id = (await applicationDbContext.Presentation.AddAsync(presentation)).Entity.Id;

        await applicationDbContext.SaveChangesAsync(cancellationToken);

        var result = await applicationDbContext.Presentation
            .FirstAsync(x => x.Id == id, cancellationToken);

        return result;
    }
}