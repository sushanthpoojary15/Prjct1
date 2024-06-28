using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Services.Exceptions;
using H2H.Physiotherapy.Services.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace H2H.Physiotherapy.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error) when (error is SqlException exec || error is DbException)
            {
                _logger.LogError(null,error, error.Message);

                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new ApiResponse
                {
                    Message = "Unable to fetch data from database",
                    ResponseStatus = "Error"
                });

                await response.WriteAsync(result);
            }
            catch (Exception error) when (error is DataNotFoundException || error is InvalidChangesException || error is InvalidUserDataException)
            {
                _logger.LogError(null,error, error.Message);
                var response = httpContext.Response;
                response.ContentType = "application/json";


                var result = JsonSerializer.Serialize(new ApiResponse() { Message = error.Message, ResponseStatus = "Error" });
                await response.WriteAsync(result);
            }
            catch (Exception error)
            {
                
                _logger.LogError(null,error, error.Message);
                var response = httpContext.Response;
                response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new ApiResponse() { Message = "Internal Error", ResponseStatus = "Error" });
                await response.WriteAsync(result);

            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
}
