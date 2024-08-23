using DPoll.Application.Slides.Commands.UpdateSlideIndex;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Slides.Commands.UpdateSlideIndex;
internal sealed class UpdateSlideIndexHandler(
    IApplicationDbContext applicationDbContext) : IRequestHandler<UpdateSlideIndexCommand, bool>
{
    public async Task<bool> Handle(UpdateSlideIndexCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var slide = await applicationDbContext.Slide
                      .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

            ArgumentNullException.ThrowIfNull(slide);
            IQueryable<Slide> affectedSlides;
            int currentIndex = slide.Index;

            bool isDirectionDownwards = currentIndex < command.Index;
            int indexModify = isDirectionDownwards ? -1 : 1;

            if (isDirectionDownwards)
                affectedSlides = applicationDbContext.Slide.Where(s => s.Index > currentIndex && s.Index <= command.Index);
            else
                affectedSlides = applicationDbContext.Slide.Where(s => s.Index < currentIndex && s.Index >= command.Index);

            await affectedSlides.ForEachAsync(s => s.Index += indexModify, cancellationToken);

            slide.Index = command.Index;

            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}