using DPoll.Application.Common;
using DPoll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoll.Application.Slides.Commands.UpdateSlideIndex
{
    public class UpdateSlideIndexCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
    }
    public class UpdateSlideIndexHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<UpdateSlideIndexCommand, bool>
    {
        public async Task<bool> Handle(UpdateSlideIndexCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var slide = await applicationDbContext.Slides
                .FirstOrDefaultAsync(y => y.Id == request.Id);


                IQueryable<Slide> affectedSlides;
                int currentIndex = slide.Index;
                bool isDirectionDownwards = currentIndex < request.Index;
                int indexModify = isDirectionDownwards ? -1 : 1;

                if (isDirectionDownwards)
                    affectedSlides = applicationDbContext.Slides.Where(s => s.Index > currentIndex && s.Index <= request.Index);
                else
                    affectedSlides = applicationDbContext.Slides.Where(s => s.Index < currentIndex && s.Index >= request.Index);

             //   await affectedSlides.ForEachAsync(s => s.Index += indexModify, cancellationToken);
             //   slide.Index = request.Index;

                await applicationDbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
