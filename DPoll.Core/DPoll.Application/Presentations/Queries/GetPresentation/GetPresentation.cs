using DPoll.Domain.Entities;
using MediatR;


namespace DPoll.Application.Presentations.Queries.GetPresentation;
public class GetPresentationsQuery : IRequest<List<Presentation>> { }

