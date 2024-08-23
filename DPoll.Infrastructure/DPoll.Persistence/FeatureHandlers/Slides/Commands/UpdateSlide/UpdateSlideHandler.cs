using DPoll.Application.Slides.Commands.UpdateSlide;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Slides.Commands.UpdateSlide;
internal sealed class InsertSlideHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<UpdateSlideCommand, bool>
{
    public async Task<bool> Handle(UpdateSlideCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var slide = await applicationDbContext.Slide
                .FirstOrDefaultAsync(y => y.Id == request.Id, cancellationToken);

            if (slide is null)//bura düzeltcesss
            {
                throw new Exception("");
            }

            slide.Type = request.Type;
            slide.Content = request.Content;
            slide.IsVisible = request.IsVisible;

            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

