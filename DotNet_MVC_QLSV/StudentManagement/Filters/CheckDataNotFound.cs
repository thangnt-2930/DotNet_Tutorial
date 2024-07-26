using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Student.Data;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class CheckDataNotFoundAttribute : ActionFilterAttribute
{
    private readonly string _id;

    public CheckDataNotFoundAttribute(string id)
    {
        _id = id;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var id = context.ActionArguments[_id] as int?;
        if (id == null || !StudentExists(id.Value, context))
        {
            context.Result = new NotFoundResult();
        }
    }

    private bool StudentExists(int id, ActionExecutingContext context)
    {
        var dbContext = (StudentContext)context.HttpContext.RequestServices.GetService(typeof(StudentContext));
        return dbContext.Students.Any(e => e.Id == id);
    }
}
