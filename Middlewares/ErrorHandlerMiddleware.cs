using System.Net;
using System.Text.Json;
using Sim_Forum.DTOs.Errors;

namespace Sim_Forum.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                // Uniformisation pour 401 / 403 sans body
                if ((context.Response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                     context.Response.StatusCode == (int)HttpStatusCode.Forbidden) &&
                     !context.Response.HasStarted)
                {
                    string defaultMessage = context.Response.StatusCode == 401 ? "Unauthorized" : "Forbidden";

                    var error = new ErrorResponseDto
                    {
                        statusCode = context.Response.StatusCode,
                        message = defaultMessage
                    };

                    await WriteJsonResponse(context, error);
                }
            }
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    await HandleExceptionAsync(context, ex);
                }
                else
                {
                    _logger.LogError(ex, "Erreur interceptée après que la réponse ait commencé");
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Erreur interceptée par middleware");

            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case KeyNotFoundException:
                    status = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;
                case ArgumentException:
                    status = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                case ApplicationException:  
                    status = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                 case InvalidOperationException:
                    status = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "Une erreur est survenue.";
                    break;
            }

            var error = new ErrorResponseDto
            {
                statusCode = (int)status,
                message = message
            };

            return WriteJsonResponse(context, error);
        }

        private Task WriteJsonResponse(HttpContext context, ErrorResponseDto error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.statusCode;

            var result = JsonSerializer.Serialize(error);
            return context.Response.WriteAsync(result);
        }
    }
}
