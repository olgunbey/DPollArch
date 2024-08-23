using DPoll.Application.Common;
using DPoll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DPoll.Application.Slides.Commands.CreateSlide
{
    public record CreateSlideCommand : IRequest<Slide>
    {
        public Guid PresentationId { get; init; }
        public string Type { get; set; }
        public JsonDocument Content { get; init; }
    }
    public class CreateSlideHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<CreateSlideCommand, Slide>
    {
        public async Task<Slide> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
        {
            //if (!await PresentationsRepository.PresentationExists(request.PresentationId, cancellationToken))
            //{
            //    NotFoundException.Throw(EntityType.Presentation);
            //}

            var presentation = await applicationDbContext.Presentations
                .AsNoTracking()
                .Where(y => y.Id == request.PresentationId)
                .FirstOrDefaultAsync(cancellationToken);


            var lastIndex = 0;
            var slideExists= applicationDbContext.Slides.Any(y=>y.PresentationId == request.PresentationId);
            if (slideExists)
                lastIndex=await applicationDbContext.Slides.MaxAsync(y=>y.Index,cancellationToken);

            var slide = new Slide(PresentationId: request.PresentationId, Index: lastIndex + 1, Type: request.Type, Content: request.Content);


           await applicationDbContext.Slides.AddAsync(slide);
           await applicationDbContext.SaveChangesAsync(cancellationToken);

            return slide;

            //return await SlidesRepository
            //    .CreateSlide(request.PresentationId, request.Type, request.Content, cancellationToken);
        }
    }
}
