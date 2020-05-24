using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;

namespace Showroom.Shared.Filters
{
    public class ActionResultFilter : IResultFilter
    {
        private readonly ILogger<ActionResultFilter> Logger;
        private readonly IOptions<JsonSerializerOptions> JsonOptions;

        public ActionResultFilter(
            ILogger<ActionResultFilter> logger,
            IOptions<JsonSerializerOptions> jsonOptions)
        {
            Logger = logger;
            JsonOptions = jsonOptions;
        }

        // OnResultExecuted – This method is called after a controller action result is executed.
        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Get response body from stream
            var responseBody = JsonSerializer.Serialize(Convert.ChangeType(context.Result, context.Result.GetType()), JsonOptions.Value);

            string message =
                $"ActionFilter: OnResultExecuted\r\n" +
                $"Controller: {context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}\r\n" +
                $"ResponseBody: {responseBody}";

            Logger.LogInformation(message);
        }

        // OnResultExecuting – This method is called before a controller action result is executed.
        public void OnResultExecuting(ResultExecutingContext context)
        {
            string message =
                $"ActionFilter: OnResultExecuting\r\n" +
                $"Controller: {context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}";

            Logger.LogInformation(message);
        }
    }
}
