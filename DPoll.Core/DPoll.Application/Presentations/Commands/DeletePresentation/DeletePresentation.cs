using MediatR;

namespace DPoll.Application.Presentations.Commands.DeletePresentation;
public class DeletePresentationCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}
public interface IDeletePresentationHandler
{

}
