﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ServerExceptions;
using TravelAPI.Common.Interfaces;

namespace TravelAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger = logger;
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var error = DefineHttpError(ex);

            if (error.StatusCode >= 500)
            {
                if (ex is CriticalServerException crEx)
                {
                    _logger.Fatal(crEx.Description, crEx.OriginalExc ?? crEx);
                }
                else
                {
                    _logger.Error(ex);
                }
                await WriteHttpResponseAsync(context, error);
            }
            else
            {
                if (error.NeedToLog)
                {
                    _logger.Error(error.Details.Text, ex);
                }
                await WriteHttpResponseAsync(context, error);
            }
        }

        private async Task WriteHttpResponseAsync(HttpContext context, HttpErrorModel error)
        {
            context.Response.StatusCode = error.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error.Details }), Encoding.UTF8);
        }

        private HttpErrorModel DefineHttpError(Exception exception)
        {
            switch (exception)
            {
                //TODO add cases and custom exceptions
                default:
                    return new HttpErrorModel
                    {
                        Details = new HttpErrorModel.ErrorDetails
                        { Text = "Undefined server error!" },
                        StatusCode = 500
                    };
            }
        }

        private class HttpErrorModel
        {
            public int StatusCode;
            public bool NeedToLog;
            public ErrorDetails Details = new ErrorDetails();

            public class ErrorDetails
            {
                public string Text;
                public object Details;
            }
        }
    }
}