using DPoll.Application.Common.Exceptions;
using DPoll.Application.Users.Queries.GetUserById;
using DPoll.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DPoll.Presentation.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/user")
            //.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("user")
            .WithDescription("Lookup, Find and Manipulate Users")
            .WithOpenApi();

        _ = root.MapGet("/", GetUsers)
            .Produces<List<Domain.Entities.User>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all Users")
            .WithDescription("\n    GET /user");

        _ = root.MapGet("/{id}", GetUserById)
            .Produces<Domain.Entities.User>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup a User by their Id")
            .WithDescription("\n    GET /User/00000000-0000-0000-0000-000000000000");

        return app;
    }
    public static async Task<IResult> GetUsers([FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new GetUsersQuery()));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    public static async Task<IResult> GetUserById([FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new GetUserByIdQuery
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
}
