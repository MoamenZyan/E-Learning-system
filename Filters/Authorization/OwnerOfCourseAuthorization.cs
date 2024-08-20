using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Learning_Platform_API.Filters.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OwnerOfCourseAuthorization : Attribute, IAsyncActionFilter
    {
        private readonly ICourseService _courseService;

        public OwnerOfCourseAuthorization(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var instructorId = Convert.ToInt32(context.HttpContext.User.Claims.First().Value);
            if (instructorId == 0)
                context.Result = new UnauthorizedResult();

            if (context.ActionArguments.TryGetValue("courseId", out var courseId))
            {
                Console.WriteLine(courseId + " " +  instructorId);
                var course = await _courseService.GetCourseById(Convert.ToInt32(courseId));
                if (course == null)
                    context.Result = new BadRequestResult();
                else if (course.InstructorId == instructorId)
                    await next();
            }
            else
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
