using DPoll.Application.Common.Exceptions;
using DPoll.Application.Dtos.PresentationRequest;
using DPoll.Application.Presentations.Commands.CreatePresentation;
using DPoll.Application.Presentations.Commands.DeletePresentation;
using DPoll.Application.Presentations.Commands.UpdatePresentation;
using DPoll.Application.Presentations.Queries.GetPresentationById;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace DPoll.Presentation.Endpoints;

public static class PresentationEndpoint
{
    public static WebApplication MapPresentationEndpoints(this WebApplication app)
    {

        var root = app.MapGroup("/api/presentation")
            //.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("presentation")
            .WithDescription("Lookup, Find and Manipulate Presentations")
            .WithOpenApi();

        _ = root.MapGet("/", GetPresentations)
            .Produces<List<DPoll.Domain.Entities.Presentation>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all Presentations")
            .WithDescription("\n    GET /presentation");

        _ = root.MapGet("/{id}", GetPresentationById)
            .Produces<Domain.Entities.Presentation>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup a Presentation by its Id")
            .WithDescription("\n    GET /presentation/00000000-0000-0000-0000-000000000000");

        _ = root.MapPost("/", CreatePresentation)
           .Produces<Domain.Entities.Presentation>(StatusCodes.Status201Created)
           .ProducesProblem(StatusCodes.Status500InternalServerError)
           .ProducesValidationProblem()
           .WithSummary("Create a Presentation")
           .WithDescription("\n    POST /presentation\n     {         \"userId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",         \"title\": \"Awesome Movies to Share with my fellow coworkers\"}");

        _ = root.MapPut("/{id}", UpdatePresentation)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Update a Presentation")
            .WithDescription("\n    PUT /presentation/00000000-0000-0000-0000-000000000000\n     {          \"userId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",         \"title\": \"Awesome Movies to Share with my fellow coworkers\"}");

        _ = root.MapDelete("/{id}", DeletePresentation)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Delete a Presentation by its Id")
            .WithDescription("\n    DELETE /presentation/00000000-0000-0000-0000-000000000000");

        return app;
    }
    public static async Task<IResult> GetPresentations([FromServices] IMediator mediator)
    { 
        try
        {
            return Results.Ok(await mediator.Send(new Application.Presentations.Queries.GetPresentation.GetPresentationsQuery()));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    public static async Task<IResult> CreatePresentation([FromBody] CreatePresentationRequest presentation, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new CreatePresentationCommand
            {
                UserId = presentation.UserId,
                Title = presentation.Title
            });

            return Results.Created($"/api/presentation/{response.Id}", response);
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetPresentationById([FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new GetPresentationByIdQuery
            {
                Id = id
            }));
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> UpdatePresentation([FromRoute] Guid id, [FromBody] UpdatePresentationRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new UpdatePresentationCommand
            {
                Id = id,
                UserId = request.UserId,
                Title = request.Title
            });

            return Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> DeletePresentation([FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new DeletePresentationCommand
            {
                Id = id,
            });

            return Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

}
