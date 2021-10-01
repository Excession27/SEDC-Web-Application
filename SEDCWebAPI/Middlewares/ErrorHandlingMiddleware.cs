using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SEDCWebAPI.Helpers;
using SEDCWebApplication.BLL.Logic.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SEDCWebAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            UserDTO user = (UserDTO)context.Items["User"];
            if( user == null )
            {
                await _next(context);
            }
            else
            {
                await HandleUserRequest(context, user);
            }
        }

        private async Task HandleUserRequest(HttpContext context, UserDTO user)
        {
            try
            {
                LogEntryProperties log = new LogEntryProperties
                {
                    User = user.UserName,
                    Date = DateTime.Now,
                    Message = $"User with id {user.UserId} call {context.Request.Path}"
                };
            }
            catch (Exception ex)
            {
                await HandleError(context, ex, user);
            }
        }

        private Task HandleError(HttpContext context, Exception ex, UserDTO user)
        {
            LogEntryProperties log = new LogEntryProperties
            {
                User = user.UserName,
                Date = DateTime.Now,
                Message = ex.Message
            };

            _logger.Error(log.ToString());

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(log.ToString());
        }
    }
}
