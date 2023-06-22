using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using PatientMs.Controller.Dto;

namespace PatientMs
{
    public static class ErrorHandler
    {
        public static void MapExceptions(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                var feature = context.Features.GetRequiredFeature<IExceptionHandlerPathFeature>();

                var error = feature.Error;
                // pakt de error uit de middleware, kijkt wat voor type het is
                _ = error switch
                {
                    KeyNotFoundException _ => context.handle(404),
                    ArgumentException _ => context.handle(400),                             //hier komt een interessant probleem: hoe handel je een 500 error af in een service waar je
                    _ => context.handle(500, "There was an error processing your request.")// geen controle over hebt? er is geen HTTP error code voor "er ging iets fout, ik kan er                                                                                           // ook niks aan doen, sorry"
                };
            });
        }

        private async static Task handle(this HttpContext context, int statusCode)
        {
            await handle(context, statusCode, context.Features.Get<IExceptionHandlerPathFeature>()?.Error.Message ?? "Error");
        }

        private async static Task handle(this HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var dto = new ErrorDto(message);
            await context.Response.WriteAsJsonAsync(dto);
        }
    }
}
