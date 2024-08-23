using DPoll.Application.Presentations.Commands.DeletePresentation;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoll.Persistence.FeatureHandlers.Presentations.Commands.DeletePresentation;
internal sealed class DeletePresentationHandler(IApplicationDbContext applicationDbContext) : IDeletePresentationHandler, IRequestHandler<DeletePresentationCommand, bool>
{
    public async Task<bool> Handle(DeletePresentationCommand command, CancellationToken cancellationToken)
    {

        if (!await applicationDbContext.Presentation.AnyAsync(y => y.Id == command.Id, cancellationToken))
        {
            ArgumentNullException.ThrowIfNull("nullls");
        }
        try
        {
            var deletedPresentation = await applicationDbContext.Presentation.SingleAsync(y => y.Id == command.Id, cancellationToken);
            applicationDbContext.Presentation.Remove(deletedPresentation);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
