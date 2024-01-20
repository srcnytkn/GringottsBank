using GringottsBank.Model.DTO;
using Newtonsoft.Json;
using System.Text;

namespace GringottsBank.Infrastructure
{
    public class CustomUnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomUnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                // Customize your 401 response here
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var errorResponse = ServiceResponse<CustomerDTO>.CreateError("Unauthorized.");
                var json = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(json, Encoding.UTF8);
            }
        }
    }
}
