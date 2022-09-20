using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CompanyEmployees.ActionFilters
{
    public class ValidationActionAttribute : IActionFilter
    {
        private readonly ILoggerManager _logger;

        public ValidationActionAttribute(ILoggerManager logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var parameter = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("")).Value;

            if (parameter == null)
            {
                context.Result = new BadRequestObjectResult($"Input: {parameter} can not be null. Controller : {controller}. Action: {action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

       
    }
}
