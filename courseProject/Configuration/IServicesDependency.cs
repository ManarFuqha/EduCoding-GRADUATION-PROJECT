using courseProject.Services.ContactUs;
using courseProject.Services.Courses;
using courseProject.Services.Employees;
using courseProject.Services.Events;
using courseProject.Services.Feedbacks;
using courseProject.Services.Instructors;
using courseProject.Services.Lectures;
using courseProject.Services.Materials;
using courseProject.Services.Reports.EXCEL;
using courseProject.Services.Reports.PDF;
using courseProject.Services.Requests;
using courseProject.Services.Skill;
using courseProject.Services.StudentCourses;
using courseProject.Services.Students;
using courseProject.Services.SubAdmins;
using courseProject.Services.Submissions;
using courseProject.Services.Users;

namespace courseProject.Configuration
{
    public static class IServicesDependency
    {
        

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ISkillsServices), typeof(SkillsServices));
            services.AddScoped<ICourseServices ,CourseServices>();
            services.AddScoped<IEventServices, EventServices>();
            services.AddScoped<ISubAdminServices, SubAdminServices>();
            services.AddScoped<IinstructorServices, instructorServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IMaterialServices, MaterialServices>();
            services.AddScoped<IStudentCoursesServices, StudentCoursesServices>();
            services.AddScoped<ILectureServices, LectureServices>();
            services.AddScoped<IRequestServices, RequestServices>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<ISubmissionServices, SubmissionServices>();
            services.AddScoped<IFeedbackServices, FeedbackServices>();
            services.AddScoped<IPdfServices, PdfServices>();
            services.AddScoped<IExcelServices, ExcelServices>();
            services.AddScoped<IContactServices, ContactServices>();


            return services;
        }
        }
}
