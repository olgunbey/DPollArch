using DPoll.Application.Slides.Commands.InsertSlide;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Slides.Commands.InsertSlide;
internal sealed class Handler(IApplicationDbContext applicationDbContext) : IRequestHandler<InsertSlideCommand, Slide>
{
    public async Task<Slide> Handle(InsertSlideCommand request, CancellationToken cancellationToken)
    {

        var presentation = await applicationDbContext.Presentation
            .AsNoTracking()
            .FirstOrDefaultAsync(y => y.Id == request.PresentationId);


        var slideBefore = await applicationDbContext.Slide
            .AsNoTracking()
            .FirstOrDefaultAsync(y => y.Index == request.Index);

        ArgumentNullException.ThrowIfNull(slideBefore);


        var slide = new Slide
        {
            PresentationId = request.PresentationId,
            Index = slideBefore.Index + 1,
            Type = request.Type,
            Content = request.Content
        };

        var slidesAfter = applicationDbContext.Slide
           .Where(r => r.Index > request.Index);

        await slidesAfter.ForEachAsync(slide => slide.Index++);

        await applicationDbContext.Slide.AddAsync(slide);

        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return slide;
    }
}