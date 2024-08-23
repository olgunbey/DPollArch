using DPoll.Application.Common;
using DPoll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DPoll.Application.Slides.Commands.InsertSlide
{
    public class InsertSlideCommand : IRequest<Slide>
    {
        public Guid PresentationId { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }
        public JsonDocument Content { get; set; }
    }
    public class InsertSlideHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<InsertSlideCommand, Slide>
    {   //E'yi 2.index'e ekle

        //1 2 3 4 5
        //A B E C D






        //1 2 4 5 
        //1 2 3 4 

        //A B C D
        public async Task<Slide> Handle(InsertSlideCommand request, CancellationToken cancellationToken)
        {
            //var presentation = await applicationDbContext.Presentations
            //      .AsNoTracking()
            //      .Where(y => y.Id == request.PresentationId)
            //      .FirstOrDefaultAsync(cancellationToken);


            var presentation = await applicationDbContext.Presentations
                  .AsNoTracking()
                  .FirstOrDefaultAsync(y => y.Id == request.PresentationId);



            //var slideBefore = await applicationDbContext.Slides
            //    .AsNoTracking()
            //    .Where(y => y.Index == request.Index)
            //    .FirstOrDefaultAsync(cancellationToken);


            //var slideBefore = await applicationDbContext.Slides
            //   .AsNoTracking()
            //   .FirstOrDefaultAsync(y => y.Index == request.Index); //B


            var slide = new Slide(request.PresentationId, request.Index+1, request.Type, request.Content); //BURADAKİ +1 olayı


            var slidesAfter = await applicationDbContext.Slides
                .AsNoTracking()
                .Where(y => y.Index > request.Index)
                .ToListAsync(cancellationToken); //C D 


            if (!slidesAfter.Any()) //sona ekleme
            {
                int MaxIndex =await applicationDbContext.Slides.MaxAsync(y => y.Index);
               // slide.Index=MaxIndex;
            }

            //slidesAfter.ForEach(slide => slide.Index++);
           

            await applicationDbContext.Slides.AddAsync(slide);

            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return slide;


        }
    }
}
