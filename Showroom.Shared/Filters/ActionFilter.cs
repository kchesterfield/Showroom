using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Showroom.Shared.Filters
{
    public class ActionExecuteFilter<T> : IActionFilter where T : class, new()
    {
        private readonly ILogger<ActionExecuteFilter<T>> Logger;
        private readonly IOptions<JsonSerializerOptions> JsonOptions;

        public ActionExecuteFilter(
            ILogger<ActionExecuteFilter<T>> logger,
            IOptions<JsonSerializerOptions> jsonOptions)
        {
            Logger = logger;
            JsonOptions = jsonOptions;
        }

        // OnActionExecuted – This method is called after a controller action is executed.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string message =
                $"ActionFilter: OnActionExecuted\r\n" +
                $"Controller: {context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}";
            Logger.LogInformation(message);
        }

        // OnActionExecuting – This method is called before a controller action is executed.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string message =
                    $"ActionFilter: OnActionExecuting\r\n" +
                    $"Controller: {context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}";

            // Construct parameter list
            List<string> parameters = new List<string>();
            foreach (var argument in context.ActionArguments.Values.Where(v => v is T))
            {
                T model = argument as T;
                parameters.Add(JsonSerializer.Serialize(model, JsonOptions.Value));
            }
            int i = 0;
            foreach (var parameter in parameters)
            {
                message += $"\r\nParameters[{i}]: {parameter}";
                i++;
            }

            Logger.LogInformation(message);
        }
    }
}
