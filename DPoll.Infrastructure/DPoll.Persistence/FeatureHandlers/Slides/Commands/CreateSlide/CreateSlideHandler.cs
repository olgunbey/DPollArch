using DPoll.Application.Slides.Commands.CreateSlide;
using DPoll.Domain.Entities;
using DPoll.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DPoll.Persistence.FeatureHandlers.Slides.Commands.CreateSlide;
internal sealed class CreateSlideHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<CreateSlideCommand, Slide>
{
    public async Task<Slide> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
    {
        var presentation = await applicationDbContext.Presentation
            .AsNoTracking()
            .FirstOrDefaultAsync(y => y.Id == request.PresentationId, cancellationToken);


        var lastIndex = 0;
        var slideExists = await applicationDbContext.Slide.AnyAsync(r => r.PresentationId == request.PresentationId, cancellationToken);
        if (slideExists)
            lastIndex = await applicationDbContext.Slide
                .MaxAsync(r => r.Index, cancellationToken);

        var slide = new Slide
        {
            PresentationId = request.PresentationId,
            Index = lastIndex + 1,
            Type = request.Type,
            Content = request.Content,
        };

        await applicationDbContext.Slide.AddAsync(slide);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return slide;
    }
}