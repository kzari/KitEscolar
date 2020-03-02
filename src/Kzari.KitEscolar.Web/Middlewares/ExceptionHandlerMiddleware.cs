using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Kzari.KitEscolar.Web.Middlewares
{
    /// <summary>
    /// Central error/exception handler Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _request;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _request = next;
            _env = env;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._request(context);
            }
            catch (ValidationException ex)
            {
                // set http status code and content type
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = JsonContentType;

                // writes / returns error model to the response

                IEnumerable<string> errosValidacao = ex.Errors.Select(a => a.ErrorMessage);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errosValidacao));

                context.Response.Headers.Clear();
            }
            catch (ArgumentException ex)
            {
                // set http status code and content type
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = JsonContentType;

                // writes / returns error model to the response

                await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Message));

                context.Response.Headers.Clear();
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    throw;

                // set http status code and content type
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new 
                    {
                        Mensagem = $"Ops.. Ocorreu um erro inesperado."
                    }));

                //TODO: Log Exception to Db


                context.Response.Headers.Clear();
            }
        }
    }


    ///<sumary>
    /// Extension method used to add the middleware to the HTTP request pipeline
    ///</sumary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
