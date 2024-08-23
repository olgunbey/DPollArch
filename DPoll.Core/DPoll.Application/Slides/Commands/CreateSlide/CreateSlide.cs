using DPoll.Domain.Entities;
using MediatR;
using System.Text.Json;

namespace DPoll.Application.Slides.Commands.CreateSlide
{
    public class CreateSlideCommand : IRequest<Slide>
    {
        public Guid PresentationId { get; init; }
        public string Type { get; set; }
        public JsonDocument Content { get; init; }
    }

}
