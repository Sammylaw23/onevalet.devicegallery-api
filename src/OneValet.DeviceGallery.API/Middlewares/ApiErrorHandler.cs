using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OneValet.DeviceGallery.Application.Common.Exceptions;
using OneValet.DeviceGallery.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.API.Middlewares
{
    public class ApiErrorHandler
    {
        private readonly RequestDelegate _next;
        private ILogger<ApiErrorHandler> _logger;

        public ApiErrorHandler(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ApiErrorHandler>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>()
                {
                    Succeeded = false,
                    Data = "Operation failed.",
                    Messages = new List<string>()
                };

                switch (error)
                {
                    case ApiException e:
                        responseModel.Messages.Add(e.Message);
                        response.StatusCode = e.StatusCode ?? (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        responseModel.Messages.Add(e.Message);
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Data = null;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        if (responseModel.Messages.Count == 0)
                            responseModel.Messages.Add("An error occurred.");
                        break;
                }
                await response.WriteAsJsonAsync(responseModel);
            }
        }
    }
}
