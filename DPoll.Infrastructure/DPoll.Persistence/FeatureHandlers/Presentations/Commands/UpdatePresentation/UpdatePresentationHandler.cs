using DPoll.Application.Presentations.Commands.UpdatePresentation;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Presentations.Commands.UpdatePresentation;
internal sealed class UpdatePresentationHandler(IApplicationDbContext applicationDbContext) : IUpdatePresentationHandler, IRequestHandler<UpdatePresentationCommand, bool>
{
    public async Task<bool> Handle(UpdatePresentationCommand command, CancellationToken cancellationToken)
    {
        if (!await applicationDbContext.Presentation.AnyAsync(y => y.Id == command.Id, cancellationToken))
        {
            ArgumentNullException.ThrowIfNull("nulls");
        }
        if (!await applicationDbContext.User.AnyAsync(y => y.Id == command.Id, cancellationToken))
        {
            ArgumentNullException.ThrowIfNull("nulls");
        }
        try
        {
            var presentation = await applicationDbContext.Presentation.FirstOrDefaultAsync(y => y.Id == command.Id, cancellationToken);
            ArgumentNullException.ThrowIfNull(presentation);

            presentation.Title = command.Title;
            presentation.UserId = command.UserId;
            presentation.UpdatedAt = DateTime.UtcNow;

            applicationDbContext.Presentation.Update(presentation);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
