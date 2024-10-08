﻿using MediatR;
namespace DPoll.Application.Presentations.Commands.UpdatePresentation;
public class UpdatePresentationCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; init; }
}
public interface IUpdatePresentationHandler
{

}
