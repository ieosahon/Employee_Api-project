using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompanyEmployees.MiddleWares
{
    public class CustomErrorExecption
    {
        private readonly RequestDelegate _next;

        public CustomErrorExecption(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception err)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = err switch
                {
                    ApplicationException => (int)HttpStatusCode.BadRequest,// custom application error
                    ArgumentException => (int)HttpStatusCode.NotFound,// not found error
                    _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                };
                var result = JsonSerializer.Serialize(new { message = err?.Message });
                await response.WriteAsync(result);
            }
        }

    }
}