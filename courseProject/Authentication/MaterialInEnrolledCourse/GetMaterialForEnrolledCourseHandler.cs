using courseProject.Authentication.EnrolledInCourse;
using courseProject.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace courseProject.Authentication.MaterialInEnrolledCourse
{
    public class GetMaterialForEnrolledCourseHandler : AuthorizationHandler<MaterialInEnrolledCourseRequeriment>
    {
        private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory context;
        public GetMaterialForEnrolledCourseHandler(projectDbContext context, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            this.context = scopeFactory;

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MaterialInEnrolledCourseRequeriment requirement)
        {
            
            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext.GetRouteData();
            var MaterialIdAsString = httpContext.Request.Query["Id"].FirstOrDefault()
                                   ?? routeData?.Values["Id"]?.ToString();

          



            if (!string.IsNullOrEmpty(MaterialIdAsString) && Guid.TryParse(MaterialIdAsString, out var MaterialId))
            {
                //var materials = await dbContext.courseMaterials.ToListAsync();
             
              //  var material =   (await dbContext.courseMaterials.FirstOrDefaultAsync(x => x.Id == MaterialId));

                var material = await dbContext.courseMaterials.FirstOrDefaultAsync(x => x.Id == MaterialId);

                if (material != null)
                {
                    var courseId = material.courseId;
                    var consultationId = material.consultationId;
                    var userIdAsString = context.User.FindFirst("UserId")?.Value;

                    if (Guid.TryParse(userIdAsString, out var userId))
                    {
                        var instructorInCourse = await dbContext.courses.AnyAsync(c=>c.Id==courseId && c.InstructorId ==userId);
                        var enrolledInCourse = await dbContext.studentCourses.AnyAsync(sc => sc.courseId == courseId && sc.StudentId == userId );
                        var enrolledInConsultation = await dbContext.consultations.AnyAsync(cm => cm.Id == consultationId && cm.InstructorId == userId);
                        var studentInConsultation = await dbContext.StudentConsultations.AnyAsync(cm => cm.consultationId == consultationId && cm.StudentId == userId);

                        if (instructorInCourse || enrolledInCourse || enrolledInConsultation || studentInConsultation)
                        {
                            context.Succeed(requirement);
                            return;
                        }
                    }

                    else
                    {
                        context.Fail();
                    }


                }
            }
            
        }
    }
}

