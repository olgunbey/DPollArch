using MediatR;

namespace DPoll.Application.Slides.Commands.DeleteSlide;
public class DeleteSlideCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}