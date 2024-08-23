using DPoll.Application.Slides.Commands.DeleteSlide;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace DPoll.Persistence.FeatureHandlers.Slides.Commands.DeleteSlide;
internal sealed class DeleteSlideHandler(IApplicationDbContext applicationDbContext)
    : IRequestHandler<DeleteSlideCommand, bool>
{
    public async Task<bool> Handle(DeleteSlideCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var slide = await applicationDbContext.Slide
                .FirstOrDefaultAsync(y => y.Id == command.Id, cancellationToken);

            ArgumentNullException.ThrowIfNull(slide);

            var slidesAfter = applicationDbContext.Slide
                .Where(y => y.Index > slide.Index);


            await slidesAfter.ForEachAsync(slidesAfter => slidesAfter.Index--);

            applicationDbContext.Slide.Remove(slide);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}