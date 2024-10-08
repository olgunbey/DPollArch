﻿using DPoll.Application.Common.Exceptions;
using DPoll.Application.Dtos.SlideRequest;
using DPoll.Application.Slides.Commands.CreateSlide;
using DPoll.Application.Slides.Commands.DeleteSlide;
using DPoll.Application.Slides.Commands.InsertSlide;
using DPoll.Application.Slides.Commands.UpdateSlide;
using DPoll.Application.Slides.Commands.UpdateSlideIndex;
using DPoll.Application.Slides.Queries.GetSlideByIndex;
using DPoll.Application.Slides.Queries.GetSlides;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DPoll.Presentation.Endpoints;

public static class SlideEndpoint
{
    public static WebApplication MapSlideEndpoints(this WebApplication app)
    {
        var presentationGroup = app.MapGroup("/api/presentation/{presentationId:guid}/slides")
            //.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("PresentationSlides")
            .WithDescription("Lookup, Find and Manipulate Presentation Slides")
            .WithOpenApi();

        _ = presentationGroup.MapGet("/", GetSlides)
            .Produces<List<DPoll.Domain.Entities.Slide>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all Presentation Slides")
            .WithDescription("\n    GET /presentation/Slide");

        _ = presentationGroup.MapGet("/{index:int}", GetSlideByIndex)
            .Produces<DPoll.Domain.Entities.Slide>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup a Presentation Slide by its index")
            .WithDescription("\n    GET /presentation/slide/00000000-0000-0000-0000-000000000000");

        _ = presentationGroup.MapPost("/", CreateSlide)
            .Produces<DPoll.Domain.Entities.Slide>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a new Presentation Slide")
            .WithDescription("\n    POST /presentation/slide");

        _ = presentationGroup.MapPost("/{index:int}", InsertSlide)
            .Produces<DPoll.Domain.Entities.Slide>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a new Presentation Slide")
            .WithDescription("\n    POST /presentation/slide");

        var slideGroup = app.MapGroup("/api/slide/{id:guid}")
            //.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("PresentationSlides")
            .WithDescription("Create, Update and Delete Presentation Slides")
            .WithOpenApi();

        _ = slideGroup.MapPatch("/", UpdateSlide)
            .Produces<DPoll.Domain.Entities.Slide>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Perform JsonPath operations to a Presentation Slide by its index")
            .WithDescription("\n    PATCH /presentation/Slide/00000000-0000-0000-0000-000000000000");

        _ = slideGroup.MapPatch("/index", UpdateSlideIndex)
            .Produces<DPoll.Domain.Entities.Slide>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Perform JsonPath operations to a Presentation Slide by its index")
            .WithDescription("\n    PATCH /presentation/Slide/00000000-0000-0000-0000-000000000000");

        _ = slideGroup.MapDelete("/", DeleteSlide)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete a Presentation Slide by its Id")
            .WithDescription("\n    DELETE /presentation/slide/00000000-0000-0000-0000-000000000000");

        return app;
    }

    public static async Task<IResult> GetSlides([FromRoute] Guid presentationId, [FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new GetSlidesQuery
            {
                PresentationId = presentationId
            }));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetSlideByIndex([FromRoute] Guid presentationId, int index, [FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new GetSlideByIndexQuery
            {
                PresentationId = presentationId,
                Index = index
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

    public static async Task<IResult> CreateSlide([FromRoute] Guid presentationId, [FromBody] CreateSlideRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new CreateSlideCommand
            {
                PresentationId = presentationId,
                Content = request.Content,
                Type = request.Type
            });

            return Results.Created($"/api/presentation/{presentationId}/slides/{response.Index}", response);
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

    public static async Task<IResult> InsertSlide([FromRoute] Guid presentationId, [FromRoute] int index, [FromBody] CreateSlideRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new InsertSlideCommand
            {
                PresentationId = presentationId,
                Index = index,
                Content = request.Content,
                Type = request.Type
            });

            return Results.Created($"/api/presentation/{presentationId}/slides/{response.Index}", response);
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

    public static async Task<IResult> UpdateSlide([FromRoute] Guid id, [FromBody] UpdateSlideRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new UpdateSlideCommand
            {
                Id = id,
                Type = request.Type,
                Content = request.Content,
                IsVisible = request.IsVisible
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

    public static async Task<IResult> UpdateSlideIndex([FromRoute] Guid id, [FromBody] UpdateSlideIndexRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new UpdateSlideIndexCommand
            {
                Id = id,
                Index = request.Index
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

    public static async Task<IResult> DeleteSlide([FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new DeleteSlideCommand
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
