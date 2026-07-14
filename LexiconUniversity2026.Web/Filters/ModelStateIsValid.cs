using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LexiconUniversity2026.Web.Filters
{
    public class ModelStateIsValid : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ViewResult
                {
                    StatusCode = 400,
                    TempData = ((Controller)context.Controller).TempData,
                    ViewData = ((Controller)context.Controller).ViewData
                };
            }
        }
    }
}
