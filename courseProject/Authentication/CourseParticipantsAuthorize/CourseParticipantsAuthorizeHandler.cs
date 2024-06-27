using courseProject.Authentication.EnrolledInCourse;
using courseProject.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Authentication.CourseParticipantsAuthorize
{
    public class CourseParticipantsAuthorizeHandler : AuthorizationHandler<CourseParticipantsAuthorizeRequirement>
    {
        private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
       


        public CourseParticipantsAuthorizeHandler(projectDbContext context, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            dbContext = context;
            _httpContextAccessor = httpContextAccessor;
           
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CourseParticipantsAuthorizeRequirement requirement)
        {
            
            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext.GetRouteData();
            var courseOrConsultationIdString = httpContext.Request.Query["CourseId"].FirstOrDefault()
                                   ?? routeData?.Values["CourseId"]?.ToString();
            if (string.IsNullOrEmpty(courseOrConsultationIdString))
            {
                courseOrConsultationIdString = httpContext.Request.Query["ConsultationId"].FirstOrDefault()
                                   ?? routeData?.Values["ConsultationId"]?.ToString();
            }


            if (!string.IsNullOrEmpty(courseOrConsultationIdString) && Guid.TryParse(courseOrConsultationIdString, out var courseOConsultationId))
            {
                var userId = context.User.FindFirst("UserId")?.Value;
                if (userId != null)
                {
                    var enrolled = await dbContext.studentCourses.AnyAsync(sc => sc.courseId == courseOConsultationId && (sc.StudentId.ToString() == userId));
                    var foundInstructor = await dbContext.courses.AnyAsync(c => c.Id == courseOConsultationId && c.InstructorId.ToString() == userId);
                    var checkConsultation = await dbContext.consultations.AnyAsync(cm => cm.Id == courseOConsultationId && cm.InstructorId.ToString() == userId);
                    var studentInConsultation = await dbContext.StudentConsultations.AnyAsync(cm => cm.consultationId == courseOConsultationId && cm.StudentId.ToString() == userId);

                    var roleAdmin = await dbContext.users.AnyAsync(x => x.UserId.ToString() == userId && x.role.ToLower() == "admin");
                    if (enrolled || foundInstructor || checkConsultation || studentInConsultation || roleAdmin)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {

                        context.Fail();
                    }


                }
            }
            //}
        }
    
    }
}
