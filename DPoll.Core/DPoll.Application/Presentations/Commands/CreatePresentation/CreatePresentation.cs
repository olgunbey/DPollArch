using DPoll.Domain.Entities;
using MediatR;

namespace DPoll.Application.Presentations.Commands.CreatePresentation;
public class CreatePresentationCommand : IRequest<Presentation>
{
    public Guid UserId { get; init; }
    public string Title { get; init; }
}
public interface ICreatePresentationHandler
{

}

