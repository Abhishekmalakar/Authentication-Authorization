using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AuthECApi.Controllers
{
    public static class AuthorizationDemoEndpoints
    {
        public static IEndpointRouteBuilder MapAuthorizationDemoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/AdminOnly", AdminOnly);

            app.MapGet("/AdminOrTeacher",[Authorize(Roles="Admin,Teacher")] () =>
            {
                return "Admin Or Teacher";
            });
            

            app.MapGet("/LibraryMembersOnly", [Authorize(Policy = "HasLibraryID")] () =>
            {
                return "Library Members Only";
            });

            app.MapGet("/ApplyForMaternityLeave", [Authorize(Roles = "Teacher",Policy = "FemalesOnly")] () =>
            {
                return "Applies for Maternity Leave";
            });

            app.MapGet("/Under10sAndFemale", [Authorize(Policy = "Under10")]
            [Authorize(Policy = "FemalesOnly")] () =>
            { return "Under 10 And Female"; });
            return app;
        }

        [Authorize(Roles ="Admin")]
        private  static string AdminOnly()
        { return "AdminOnly"; }
    }
}
