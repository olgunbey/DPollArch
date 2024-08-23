using DPoll.Presentation.Endpoints;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DPoll.Presentation.Extensions;

[ExcludeFromCodeCoverage]
public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        #region Logging

        app.UseSerilogRequestLogging();

        #endregion Logging

        #region Security

        app.UseHsts();

        #endregion Security

        #region API Configuration

        app.UseHttpsRedirection();

        #endregion API Configuration

        #region Swagger

        var ti = CultureInfo.CurrentCulture.TextInfo;

        app.UseSwagger();
        app.UseSwaggerUI(c =>
           c.SwaggerEndpoint(
               "/swagger/v1/swagger.json",
               $"CleanMinimalApi - {ti.ToTitleCase(app.Environment.EnvironmentName)} - V1"));

        #endregion Swagger

        #region MinimalApi

        app.MapUserEndpoints();
        app.MapPresentationEndpoints();
        app.MapSlideEndpoints();

        #endregion MinimalApi

        return app;
    }
}
