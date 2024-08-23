using DPoll.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DPoll.Application.Slides.Commands.UpdateSlide
{
    public class UpdateSlideCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public bool IsVisible { get; set; }
        public JsonDocument Content { get; init; }
    }
    public class UpdateSlideHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<UpdateSlideCommand, bool>
    {
        public async Task<bool> Handle(UpdateSlideCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var slide = await applicationDbContext.Slides
                   .AsNoTracking()
                   .FirstOrDefaultAsync(y => y.Id == request.Id);

                //slide.Type = request.Type;
                //slide.Content = request.Content;

                await applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
